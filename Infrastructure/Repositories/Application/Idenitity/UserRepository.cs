﻿using Common.Enums;
using Common.Models.DataTable;
using Domain.Entities.Base.Identity;
using Infrastructure.DbContexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Application.Idenitity
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly IdentityContext _identityContext;

        public UserRepository(IdentityContext identityContext,
            UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _identityContext = identityContext;
        }

        public IQueryable<ApplicationUser> Users { get => _userManager.Users; }

        public IQueryable<ApplicationUser> Model { get => _identityContext.Users; }

        public async Task<ApplicationUser> GetUserAsync(ClaimsPrincipal principal)
        {
            return await _userManager.GetUserAsync(principal);
        }

        public ApplicationUser GetUserById(string id)
        {
            return _identityContext.Users
                   .Include(e => e.Project)
                   .Include(e => e.Bank)
                   .Where(m => m.Id == id)
                   .FirstOrDefault();
        }

        public async Task<ApplicationUser> GetUserByIdAsync(string id)
        {
            return await _identityContext.Users
                  .Include(e => e.Project)
                  .Include(e => e.Bank)
                  .Where(m => m.Id == id)
                  .FirstOrDefaultAsync();
        }

        public List<ApplicationUser> GetUserList()
        {
            return _identityContext.Users
                .Include(e => e.Project)
                .Include(e => e.Bank)
                .ToList();
        }

        public async Task<List<ApplicationUser>> GetUserListAsync()
        {
            return await _identityContext.Users
                .Include(e => e.Project)
                .Include(e => e.Bank)
                //.Where(e=>e.PersonnelCode.Contains("777777"))
                .ToListAsync();
        }

        public async Task<List<ApplicationUser>> GetUserListByProjectIdAsync(long projectId)
        {
            return await _identityContext.Users
                .Include(e => e.Project)
                .Include(e => e.Bank)
                .Where(x => x.ProjectRef == projectId && x.UserType == Common.Enums.UserType.PublicUser && !x.IsDeleted)
                .ToListAsync();
        }

        public async Task<List<ApplicationUser>> GetUserListByProjectIdAsync(long projectId, int take, int page)
        {
            return await _identityContext.Users
                .Include(e => e.Project)
                .Where(x => x.ProjectRef == projectId && x.UserType == Common.Enums.UserType.PublicUser && !x.IsDeleted)
                .Skip(take * page)
                .Take(take)
                .ToListAsync();
        }

        public async Task<DataTableDTO<IEnumerable<ApplicationUser>>> GetUserListByProjectIdDataTableAsync(long projectId, string key, int pageSize, int pageNumber, EmployeeStatus? employeeStatus, Gender? gender, MilitaryService? militaryService, MaritalStatus? maritalStatus)
        {
            var result = new DataTableDTO<IEnumerable<ApplicationUser>>();

            var data = await _identityContext.Users
                .Include(e => e.Project)
                .Include(e => e.Bank)
                .Where(x => string.IsNullOrEmpty(key)
                || EF.Functions.Like(x.PhoneNumber, $"%{key}%")
                || EF.Functions.Like(x.LastName, $"%{key}%")
                || EF.Functions.Like(x.FirstName, $"%{key}%")
                || EF.Functions.Like(x.FatherName, $"%{key}%")
                || EF.Functions.Like(x.PersonnelCode, $"%{key}%")
                || EF.Functions.Like(x.NationalCode, $"%{key}%")
                || EF.Functions.Like(x.InsuranceCode, $"%{key}%")
                || EF.Functions.Like(x.JobTitle, $"%{key}%"))
                .Where(m => gender == null || m.Gender == gender.Value)
                .Where(m => employeeStatus == null || m.EmployeeStatus == employeeStatus.Value)
                .Where(m => militaryService == null || m.MilitaryService == militaryService.Value)
                .Where(m => maritalStatus == null || m.MaritalStatus == maritalStatus.Value)
                .Where(x => (projectId == 0 || x.ProjectRef == projectId)
                //&& (string.IsNullOrEmpty(key) || x.PhoneNumber.Contains(key) || x.FullName.Contains(key) || x.PersonnelCode.Contains(key) || x.NationalCode.Contains(key))
                && x.UserType == Common.Enums.UserType.PublicUser
                && !x.IsDeleted)
                .OrderByDescending(m => m.PersonnelCode)
                .ToListAsync();

            result.DataCount = data.Count;

            result.PageSize = pageSize;

            result.PageNumber = pageNumber;

            result.PageCount = data.Count / pageSize;

            result.Model = data
                .Skip(pageSize * pageNumber)
                .Take(pageSize)
                .ToList();

            return result;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _identityContext.SaveChangesAsync();
        }
    }
}
