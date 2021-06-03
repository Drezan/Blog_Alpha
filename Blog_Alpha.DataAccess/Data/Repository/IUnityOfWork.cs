using System;
using System.Collections.Generic;
using System.Text;

namespace Blog_Alpha.Data.Data.Repository
{
    public interface IUnityOfWork : IDisposable
    {
        ICategoryRepository Category { get; }
        IArticleRepository Article { get; }

        void Save();
    }
}
