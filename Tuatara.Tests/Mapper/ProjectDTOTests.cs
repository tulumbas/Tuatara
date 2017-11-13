using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tuatara.Services.Dto;
using Tuatara.Data.Entities;
using Tuatara.Services;

namespace Tuatara.Tests.Mapper
{
    [TestClass]
    public class ProjectDTOTests
    {
        IMapper _mapper;

        [TestInitialize]
        public void Init()
        {
            var automapperConfig = new MapperConfiguration(cfg => {
                cfg.AddProfile<TuataraDataProfile>(); // add profiles for all assemblies
            });
            _mapper = automapperConfig.CreateMapper();
        }

        [TestMethod]
        public void WorkEntity2ProjectDTO()
        {
            var parent = new WorkEntity
            {
                ID = 1,
                Name = "All Products",
                SubItems = new List<WorkEntity>()
            };

            var child1 = new WorkEntity
            {
                ID = 2,
                Name = "Product 1",
                ParentID = 1,
                Parent = parent
            };

            var child2 = new WorkEntity
            {
                ID = 3,
                Name = "Product 2",
                Parent = parent,
                ParentID = 1
            };

            parent.SubItems.Add(child1);
            parent.SubItems.Add(child2);

            var dto0 = _mapper.Map<ProjectDtoWithParent>(parent);
            var dto1 = _mapper.Map<ProjectDtoWithParent>(child1);
            
            Assert.AreEqual(parent.ID, dto0.ID, "copying parent.ID");
            Assert.AreEqual(parent.Name, dto0.Name, "copying parent.name");

            Assert.AreEqual(child1.ID, dto1.ID, "copying child.ID");
            Assert.AreEqual(child1.Name, dto1.Name, "copying child.name");
        }

        [TestMethod]
        public void ProjectDTO2WorkEntity()
        {
            var dto0 = new ProjectDtoWithParent
            {
                ID = 1,
                Name = "All Products",
            };

            var dto1 = new ProjectDtoWithParent
            {
                ID = 2,
                Name = "Product 1"
            };

            var parent = _mapper.Map<WorkEntity>(dto0);
            var child = _mapper.Map<WorkEntity>(dto1);

            Assert.AreEqual(dto0.ID, parent.ID, "copying parent.ID");
            Assert.AreEqual(dto0.Name, parent.Name, "copying parent.name");

            Assert.AreEqual(dto1.ID, child.ID, "copying child.ID");
            Assert.AreEqual(dto1.Name, child.Name, "copying child.name");
        }

        [TestMethod]
        public void WorkEntity2ProjectDTOFlat()
        {
            var parent = new WorkEntity
            {
                ID = 1,
                Name = "All Products",
                SubItems = new List<WorkEntity>()
            };

            var child1 = new WorkEntity
            {
                ID = 2,
                Name = "Product 1",
                ParentID = 1,
                Parent = parent
            };

            var child2 = new WorkEntity
            {
                ID = 3,
                Name = "Product 2",
                Parent = parent,
                ParentID = 1
            };

            parent.SubItems.Add(child1);
            parent.SubItems.Add(child2);

            var dto0 = _mapper.Map<ProjectDto>(parent);
            var dto1 = _mapper.Map<ProjectDto>(child1);
            var dto2 = _mapper.Map<ProjectDto>(child2);

            Assert.AreEqual(parent.ID, dto0.ID, "copying parent.ID");
            Assert.AreEqual(parent.Name, dto0.Name, "copying parent.name");
            Assert.IsNull(dto0.ParentID, "parent.parentid");

            Assert.AreEqual(child1.ID, dto1.ID, "copying child.ID");
            Assert.AreEqual(child1.Name, dto1.Name, "copying child.name");
            Assert.IsTrue(dto1.ParentID.HasValue, "child has parent");
            Assert.AreEqual(1, dto1.ParentID.Value, "child.parentid");
           // Assert.AreEqual(parent.Name, dto1.ParentName, "child.parentname");
        }

        [TestMethod]
        public void ProjectDTOFlat2WorkEntity()
        {
            var dto0 = new ProjectDto
            {
                ID = 1,
                Name = "All Products",
            };

            var dto1 = new ProjectDto
            {
                ID = 2,
                Name = "Product 1",
                ParentID = 1,
                //ParentName = "All Products"
            };

            var parent = _mapper.Map<WorkEntity>(dto0);
            var child = _mapper.Map<WorkEntity>(dto1);

            Assert.AreEqual(dto0.ID, parent.ID, "copying parent.ID");
            Assert.AreEqual(dto0.Name, parent.Name, "copying parent.name");

            Assert.AreEqual(dto1.ID, child.ID, "copying child.ID");
            Assert.AreEqual(dto1.Name, child.Name, "copying child.name");
            Assert.IsTrue(dto1.ParentID.HasValue, "child has parent");
            Assert.AreEqual(1, dto1.ParentID.Value, "child.parentid");
        }

    }
}
