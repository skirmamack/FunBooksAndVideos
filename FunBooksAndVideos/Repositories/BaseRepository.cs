using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FunBooksAndVideos.Repositories
{
    public abstract class BaseRepository<T>
    {
        public abstract IEnumerable<T> Get();
    }
}
