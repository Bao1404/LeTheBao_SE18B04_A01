using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects
{
    public class TagDAO
    {
        private static TagDAO instance;
        private static readonly object instanceLock = new object();
        private readonly FunewsManagementContext _context;
        public static TagDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TagDAO();
                }
                return instance;
            }
        }
        public TagDAO()
        {
            _context = new FunewsManagementContext();
        }
        public List<Tag> GetAllTags()
        {
            List<Tag> list = new List<Tag>();
            try
            {
                list = _context.Tags.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }
        public Tag GetTagById(int tagId)
        {
            try
            {
                return _context.Tags.Find(tagId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
