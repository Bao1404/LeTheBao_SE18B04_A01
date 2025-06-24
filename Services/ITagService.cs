using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface ITagService
    {
        List<Tag> GetAllTags();
        Tag GetTagById(int tagId);
    }
}
