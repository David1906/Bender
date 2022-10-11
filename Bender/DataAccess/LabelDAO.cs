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
using System.Collections;
using System.Diagnostics.Contracts;

namespace Bender.DataAccess
{
    [Table("Labels")]
    public class LabelDAO : WithBenderContext
    {
        [Key]
        public int LabelId { get; set; }
        public string Name { get; set; } = "";
        public string Supplier { get; set; } = "";
        public virtual ICollection<LabelItemDAO> Items { get; set; } = new List<LabelItemDAO>();

        public LabelDAO(BenderContext? context = null) : base(context)
        {
        }

        public void Add(string name, string supplier, List<LabelItem> labelItems)
        {
            var labelDAO = new LabelDAO()
            {
                Name = name,
                Supplier = supplier,
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
        public List<Label> FindAll(bool tracking = false)
        {
            var labelsDAO = this.Context.Labels
                .Include(x => x.Items.OrderBy(x => x.Index))
                .ToList(); ;
            if (labelsDAO == null)
            {
                return new List<Label>();
            }
            return this.MapTo<List<Label>>(labelsDAO);
        }
        public Label? Find(string name, bool tracking = false)
        {
            var query = this.Context.Labels
                .Where(x => x.Name == name);
            return this.FindByQuery(query, tracking);
        }
        internal Label? FindBySupplier(string supplier, bool tracking = false)
        {
            if (string.IsNullOrWhiteSpace(supplier))
            {
                return null;
            }
            var query = this.Context.Labels
                .Where(x => x.Supplier.ToLower() == supplier.ToLower());
            return this.FindByQuery(query, tracking);
        }
        private Label? FindByQuery(IQueryable<LabelDAO> query, bool tracking)
        {
            if (tracking == false)
            {
                query = query.AsNoTracking();
            }
            var labelDAO = query
                .Include(x => x.Items.OrderBy(x => x.Index))
                .FirstOrDefault();
            if (labelDAO == null)
            {
                return null;
            }
            return this.MapTo<Label>(labelDAO);
        }
        private T MapTo<T>(LabelDAO labelDAO)
        {
            return this.MapTo<LabelDAO, T>(labelDAO);
        }
        private T MapTo<T>(List<LabelDAO> labelsDAO)
        {
            return this.MapTo<List<LabelDAO>, T>(labelsDAO);
        }
        private Tdestination MapTo<TSource, Tdestination>(TSource labelDAO)
        {
            Contract.Assert(labelDAO != null);
            var configuration = new MapperConfiguration(c =>
            {
                c.CreateMap<LabelDAO, Label>()
                     .ForMember(s => s.Items, c => c.MapFrom(m => m.Items));
                c.CreateMap<LabelItemDAO, LabelItem>();
            });
            return new MapperHelper(configuration).Map<Tdestination>(labelDAO);
        }
        private Tdestination MapFrom<TSource, Tdestination>(TSource labelDAO)
        {
            Contract.Assert(labelDAO != null);
            var configuration = new MapperConfiguration(c =>
            {
                c.CreateMap<Label, LabelDAO>()
                     .ForMember(s => s.Items, c => c.MapFrom(m => m.Items));
                c.CreateMap<LabelItem, LabelItemDAO>();
            });
            return new MapperHelper(configuration).Map<Tdestination>(labelDAO);
        }
        public void Save(Label label)
        {
            LabelDAO labelDAO = this.MapFrom<Label, LabelDAO>(label);
            if (labelDAO != null)
            {
                if (label.IsPersisted)
                {
                    this.Remove(label);
                }
                this.Context.Labels.Add(labelDAO);
                this.Context.SaveChanges();
            }
        }
        public void Remove(Label label)
        {
            var labelDAO = this.Context.Labels
                .Where(x => x.LabelId == label.LabelId)
                .FirstOrDefault();
            if (labelDAO != null)
            {
                this.Context.Labels.Remove(labelDAO);
                this.Context.SaveChanges();
            }
        }
        private void RemoveItems(Label label)
        {
            var labelItemsDAO = this.Context.LabelItems
                .Where(x => x.Label.LabelId == label.LabelId)
                .ToList();
            if (labelItemsDAO != null)
            {
                this.Context.LabelItems.RemoveRange(labelItemsDAO);
                this.Context.SaveChanges();
            }
        }
        internal List<string> FindAllNames()
        {
            return this.Context.Labels.Select(x => x.Name).ToList();
        }
    }
}
