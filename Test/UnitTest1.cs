using System;
using System.Linq;
using Lab2.Controllers;
using Lab2.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var options = new DbContextOptionsBuilder<Performance_ActorContext>().UseInMemoryDatabase(databaseName: "Data2").Options;
            var _context = new Performance_ActorContext(options);
            var controller3 = new DepartmentsController(_context);
            var controller1 = new BankGroupsController(_context);
            var controller2 = new BanksController(_context);
            var dp = new Department();
            var dp2 = new Department();
            var bnk = new Banks();
            var bgrp = new BankGroups();
            bgrp.BankGroupName = "EnotG";
            bnk.BankName = "EnotB";
            bnk.BankGroupId = 1;
            dp.BankId = 1; dp2.BankId = 1;
            dp.DepartmentName = "dp"; dp2.DepartmentName = "dp";
            _ = controller1.PostBankGroups(bgrp);
            _ = controller2.PostBanks(bnk);
            _ = controller3.PostDepartment(dp);
            _ = controller3.PostDepartment(dp2);
            int am = _context.Departments.Count();

            Assert.True(am == 1);
        }
    }
}
