using System.Linq.Expressions;

namespace Core.Interfaces;

public interface ISpecification<T>
{
    Expression<Func<T, bool>>? Criteria {get;}
    Expression<Func<T, object>>? OrderBy {get;}
    Expression<Func<T, object>>? OrderByDesending {get;}
    bool IsDistinct {get;}

    int Take {get;}
    int Skip {get;}
    bool IsPagingEnabled {get;}
    IQueryable<T> ApplyCritiria(IQueryable<T> query);
    
}

//creating projection so that we can give T ask params and return in another type
public interface ISpecification<T, TResult> : ISpecification<T>
{
    Expression<Func<T, TResult>>? Select {get;}
}
