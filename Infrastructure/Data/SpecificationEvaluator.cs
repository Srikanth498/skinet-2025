using Core.Entities;
using Core.Interfaces;

namespace Infrastructure.Data;

public class SpecificationEvaluator<T> where T : BaseEntity
{
    public static IQueryable<T> GetQuery(IQueryable<T> query, ISpecification<T> spec)
    {
        if(spec.Criteria != null)
        {
            query = query.Where(spec.Criteria);// X => x.Brand == brand.

        }

        if(spec.OrderBy != null)
        {
            query = query.OrderBy(spec.OrderBy);
        }

        if(spec.OrderByDesending != null)
        {
            query = query.OrderByDescending(spec.OrderByDesending);
        }

        if(spec.IsDistinct)
        {
            query = query.Distinct();
        }
        return query;
    }


    //TSpec is the type of the specification and TResult is the type of the result we want to return
    public static IQueryable<TResult> GetQuery<TSpec, TResult>(IQueryable<T> query, ISpecification<T, TResult> spec)
    {
        if(spec.Criteria != null)
        {
            query = query.Where(spec.Criteria);// X => x.Brand == brand.

        }

        if(spec.OrderBy != null)
        {
            query = query.OrderBy(spec.OrderBy);
        }

        if(spec.OrderByDesending != null)
        {
            query = query.OrderByDescending(spec.OrderByDesending);
        }

        var selectQuery = query as IQueryable<TResult>;

        if(spec.Select != null)
        {
            selectQuery = query.Select(spec.Select);
        }

        if(spec.IsDistinct)
        {
            selectQuery = selectQuery?.Distinct();
        }

        return selectQuery ?? query.Cast<TResult>();
    }
}
