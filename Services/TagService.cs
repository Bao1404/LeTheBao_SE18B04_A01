using BusinessObjects;
using DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class TagService : ITagService
    {
        public List<Tag> GetAllTags() => TagDAO.Instance.GetAllTags();
        public Tag GetTagById(int tagId) => TagDAO.Instance.GetTagById(tagId);
    }
}
