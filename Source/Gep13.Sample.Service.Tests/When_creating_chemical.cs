﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="When_creating_chemical.cs" company="Gary Ewan Park">
//   Copyright (c) Gary Ewan Park, 2014, All rights reserved.
// </copyright>
// <summary>
//   Defines the When_creating_chemical type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Gep13.Sample.Service.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using AutoMapper;

    using Gep13.Sample.Data.Infrastructure;
    using Gep13.Sample.Data.Repositories;
    using Gep13.Sample.Model;

    using NSubstitute;

    using NUnit.Framework;

    [TestFixture]
    public class When_creating_chemical
    {
        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            Mapper.CreateMap<ChemicalDTO, Chemical>();
            Mapper.CreateMap<Chemical, ChemicalDTO>();
        }

        [Test]
        public void Should_create_chemical()
        {
            var fakeRepository = Substitute.For<IChemicalRepository>();
            var fakeUnitOfWork = Substitute.For<IUnitOfWork>();
            var chemicalService = new ChemicalService(fakeRepository, fakeUnitOfWork);

            var toAdd = new ChemicalDTO
            {
                Balance = 110.99,
                Name = "First"
            };

            fakeRepository.GetMany(Arg.Any<Expression<Func<Chemical, bool>>>()).ReturnsForAnyArgs(x => new List<Chemical>());
            
            fakeRepository.Add(Arg.Do<Chemical>(x => x.Id = 1)).Returns(new Chemical { Id = 1 });
            
            var chemical = chemicalService.AddChemical("First", 110.99);

            Assert.That(chemical.Id, Is.EqualTo(1));
            fakeUnitOfWork.Received().SaveChanges();
        }
    }
}