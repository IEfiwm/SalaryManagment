using Domain.Entities.Basic;
using Infrastructure.Repositories.Application.Basic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Abstractions;
using Web.Areas.Admin.Models;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Admin,Manager")]
    public class BankController : BaseController<UserController>
    {

        private readonly IBankRepository _bankRepository;

        private readonly IBank_AccountRepository _bank_AccountRepository;

        public BankController(IBankRepository bankRepository, IBank_AccountRepository bank_AccountRepository)
        {
            _bankRepository = bankRepository;
            _bank_AccountRepository = bank_AccountRepository;
        }

        public async Task<IActionResult> Index()
        {
             return View();
        }
        public async Task<IActionResult> LoadAll()
        {
            var bankAccounts = await _bank_AccountRepository.GetListAsync();

            var model = _mapper.Map<IEnumerable<Bank_AccountViewModel>>(bankAccounts);
            
            var banks = await _bankRepository.GetListAsync();
            ViewData["banks"] = _mapper.Map<List<BankViewModel>>(banks.Where(x => x.Active).ToList());

            return PartialView("_ViewAll", model);
        }
        public async Task<IActionResult> Create()
        {
            var banks = await _bankRepository.GetListAsync();
            ViewData["banks"] = _mapper.Map<List<BankViewModel>>(banks.Where(x => x.Active).ToList());
            return View();
        }

        public async Task<IActionResult> Edit(long bankAccountId)
        {
            var banks = await _bankRepository.GetListAsync();
            var model = await _bank_AccountRepository.GetByIdAsync(bankAccountId);

            ViewData["banks"] = _mapper.Map<List<BankViewModel>>(banks.Where(x => x.Active).ToList());

            return View(_mapper.Map<Bank_AccountViewModel>(model));
        }

        [HttpPost]
        public async Task<IActionResult> Create(Bank_AccountViewModel model)
        {
            var res = await _bank_AccountRepository.InsertAndSaveAsync(_mapper.Map<Bank_Account>(model));

            if (res > 0)
                _notify.Success("عملیات با موفقیت انجام شد.");
            else
                _notify.Error("عملیات با خطا مواجعه شد.");

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Bank_AccountViewModel model)
        {
            var modelAcc = _mapper.Map(model, await _bank_AccountRepository.GetByIdAsync(model.Id));

            await _bank_AccountRepository.UpdateAsync(modelAcc);

            var res = await _bank_AccountRepository.SaveChangesAsync();

            if (res > 0)
                _notify.Success("عملیات با موفقیت انجام شد.");
            else
                _notify.Error("عملیات با خطا مواجعه شد.");

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int bankAccountId)
        {
            var model = await _bank_AccountRepository.GetByIdAsync(bankAccountId);

            model.IsDeleted = true;

            await _bank_AccountRepository.UpdateAsync(model);

            var res = await _bank_AccountRepository.SaveChangesAsync();

            if (res > 0)
                _notify.Success("حذف حساب بانکی با موفقیت انجام شد.");
            else
                _notify.Error("حذف حساب بانکی انجام نشد.");

            return RedirectToAction("Index");
        }

    }
}