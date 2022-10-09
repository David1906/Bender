using AutoMapper;
using Bender.Helpers;
using Bender.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;

namespace Bender.DataAccess
{
    [Table("Labels")]
    public class LabelDAO : WithBenderContext
    {
        [Key]
        public int LabelId { get; set; }
        public string Name { get; set; } = "";
        public virtual ICollection<LabelItemDAO> Items { get; set; } = new List<LabelItemDAO>();

        public LabelDAO(BenderContext? context = null) : base(context)
        {
        }

        public void Add(string name, List<LabelItem> labelItems)
        {
            var labelDAO = new LabelDAO()
            {
                Name = name,
                Items = MapperHelper
                    .Create<LabelItem, LabelItemDAO>()
                    .Map<List<LabelItemDAO>>(labelItems)
            };
            this.Context.Labels.Add(labelDAO);
            this.Context.SaveChanges();
        }
        public bool IsTableEmpty()
        {
            return this.Context.Labels.Count() <= 0;
        }
        public void Clear()
        {
            foreach (var item in this.Context.Labels)
            {
                this.Context.Labels.Remove(item);
            }
            this.Context.SaveChanges();
        }
        public void Seed()
        {
            new LabelSeeder(this.Context).Seed();
        }
        public Label? Find(string name)
        {
            var labelDAO = this.Context.Labels
                .Where(x => x.Name == name)
                .Include(x => x.Items.OrderBy(x => x.Index))
                .FirstOrDefault();
            if (labelDAO == null)
            {
                return null;
            }
            return this.MapToLabel(labelDAO);
        }
        private Label MapToLabel(LabelDAO labelDAO)
        {
            var configuration = new MapperConfiguration(c =>
            {
                c.CreateMap<LabelDAO, Label>()
                     .ForMember(s => s.Items, c => c.MapFrom(m => m.Items));
                c.CreateMap<LabelItemDAO, LabelItem>();
            });
            return new MapperHelper(configuration).Map<Label>(labelDAO);
        }

        internal List<string> FindAllNames()
        {
            return this.Context.Labels.Select(x => x.Name).ToList();
        }
    }
}
