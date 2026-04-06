using Microsoft.EntityFrameworkCore;
using SayyadCo.Domain.Common;
using SayyadCo.Domain.Interfaces;
using SayyadCo.Infrastructure.Data;
using System.Linq.Expressions;
using System.Reflection;

namespace SayyadCo.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<T> _dbSet;


        public Repository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        protected virtual IQueryable<T> GetAllQueryable()
            => _dbSet.AsNoTracking();

        protected virtual IQueryable<T> GetByIdQueryable()
            => _dbSet.AsNoTracking();

        public async Task AddAsync(T entity)
            => await _dbSet.AddAsync(entity);
        public void Update(T entity)
            => _dbSet.Update(entity);

        public void Remove(T entity)
            => _dbSet.Remove(entity);

        public async Task<T?> GetByIdAsync(string id)
            => await GetByIdQueryable().FirstOrDefaultAsync(e => e.Id == id);

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
            => await _dbSet.Where(predicate).ToListAsync();

        public async Task<PagedResult<T>> GetAllAsync(QueryParameters parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            var query = GetAllQueryable();

            query = ApplyPropertyFilters(query, parameters);

            if (!string.IsNullOrWhiteSpace(parameters.SearchTerm))
            {
                query = ApplySearch(query, parameters.SearchTerm);
            }

            if (!string.IsNullOrWhiteSpace(parameters.OrderBy))
            {
                query = ApplySorting(query, parameters.OrderBy, parameters.IsDescending);
            }

            var totalCount = await query.CountAsync();

            var items = await query
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .ToListAsync();

            return new PagedResult<T>(
                items,
                totalCount,
                parameters.PageNumber,
                parameters.PageSize
            );
        }

        private IQueryable<T> ApplyPropertyFilters(IQueryable<T> query, QueryParameters parameters)
        {
            var paramType = parameters.GetType();
            var entityType = typeof(T);

            var filterProperties = paramType.GetProperties()
                .Where(p => p.Name != "PageNumber" &&
                           p.Name != "PageSize" &&
                           p.Name != "SearchTerm" &&
                           p.Name != "OrderBy" &&
                           p.Name != "IsDescending")
                .ToList();

            foreach (var paramProp in filterProperties)
            {
                var paramValue = paramProp.GetValue(parameters);

                if (paramValue == null)
                    continue;

                if (paramProp.PropertyType == typeof(string) && string.IsNullOrWhiteSpace(paramValue.ToString()))
                    continue;

                var entityProp = entityType.GetProperty(paramProp.Name,
                    BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);

                if (entityProp == null)
                    continue;

                query = ApplyEqualityFilter(query, entityProp.Name, paramValue);
            }

            return query;
        }

        private IQueryable<T> ApplyEqualityFilter(IQueryable<T> query, string propertyName, object value)
        {
            var parameter = Expression.Parameter(typeof(T), "e");
            var property = Expression.Property(parameter, propertyName);
            var constant = Expression.Constant(value);

            Expression comparison;

            if (property.Type != value.GetType())
            {
                var convertedConstant = Expression.Convert(constant, property.Type);
                comparison = Expression.Equal(property, convertedConstant);
            }
            else
            {
                comparison = Expression.Equal(property, constant);
            }

            var lambda = Expression.Lambda<Func<T, bool>>(comparison, parameter);
            return query.Where(lambda);
        }

        protected virtual IQueryable<T> ApplySearch(IQueryable<T> query, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return query;

            var searchLower = searchTerm.ToLower();

            var stringProperties = typeof(T)
                .GetProperties()
                .Where(p => p.PropertyType == typeof(string))
                .ToList();

            if (!stringProperties.Any())
                return query;

            var parameter = Expression.Parameter(typeof(T), "e");
            Expression? combinedExpression = null;

            foreach (var property in stringProperties)
            {
                var propertyAccess = Expression.Property(parameter, property);
                var toLowerMethod = typeof(string).GetMethod("ToLower", Type.EmptyTypes);
                var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });

                if (toLowerMethod == null || containsMethod == null)
                    continue;

                var notNullCheck = Expression.NotEqual(propertyAccess, Expression.Constant(null, typeof(string)));
                var toLowerCall = Expression.Call(propertyAccess, toLowerMethod);
                var containsCall = Expression.Call(toLowerCall, containsMethod, Expression.Constant(searchLower));
                var condition = Expression.AndAlso(notNullCheck, containsCall);

                combinedExpression = combinedExpression == null
                    ? condition
                    : Expression.OrElse(combinedExpression, condition);
            }

            if (combinedExpression != null)
            {
                var lambda = Expression.Lambda<Func<T, bool>>(combinedExpression, parameter);
                query = query.Where(lambda);
            }

            return query;
        }

        private IQueryable<T> ApplySorting(IQueryable<T> query, string? orderBy, bool isDescending)
        {
            if (string.IsNullOrWhiteSpace(orderBy))
                return query;

            var property = typeof(T)
                .GetProperties()
                .FirstOrDefault(p => p.Name.Equals(orderBy, StringComparison.OrdinalIgnoreCase));

            if (property == null)
                return query;

            var parameter = Expression.Parameter(typeof(T), "e");
            var propertyAccess = Expression.Property(parameter, property);
            var lambda = Expression.Lambda(propertyAccess, parameter);

            var methodName = isDescending ? "OrderByDescending" : "OrderBy";

            var resultExpression = Expression.Call(
                typeof(Queryable),
                methodName,
                new Type[] { typeof(T), property.PropertyType },
                query.Expression,
                Expression.Quote(lambda)
            );

            return query.Provider.CreateQuery<T>(resultExpression);
        }
    }
}
