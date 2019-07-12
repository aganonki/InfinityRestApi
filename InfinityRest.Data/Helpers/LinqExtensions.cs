using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace InfinityRest.Data.Helpers
{

    public static class LinqExtensions
    {
        public static TOutput SelectSingle<TInput, TOutput>(
                this TInput obj,
                Expression<Func<TInput, TOutput>> expression)
        {
            return expression.Compile()(obj);
        }
    }
}
