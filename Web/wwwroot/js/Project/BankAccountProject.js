
function AddBankBox() {

    var index = document.getElementsByName('BankBox').length;

    var newBox = `<div class="row" name="BankBox" id="Box_` + index + `">
                        <div class="col-6 mt-3">
                            <label class="form-label" for="DisplayName">بانک</label>
                            <select id="bank" class="js-data-example-ajax form-control" name="BankId" onchange="changeBank(this,`+ index + `)" style="background-color: #ffffff!important;color:black;border:1px solid #ced4da">
                            <option selected>یک مورد را انتخاب کنید</option>`;
                                    bankLists.forEach(function (item, index) {
                                        newBox += ` <option value="` + item.id + `">` + item.title + `</option>`
                                    });
        newBox += ` </select>
                            </div>
                            <div class="col-5 mt-3">
                                <label class="form-label" for="BankAccounts">شماره حساب</label>
                                <select id="BankAccounts_`+ index + `" class="js-data-example-ajax form-control" name="BankAccounts" style="background-color: #ffffff!important;color:black;border:1px solid #ced4da">
                                </select>
                            </div>
                            <div class="col-1 mt-5">
                                <button type="button" onclick="RemoveBox(`+ index + `)" class="btn btn-danger">
                                    <i>حذف </i>
                                </button>
                            </div>
                        </div>`;

    $('#mainBox').after(newBox);

}

function RemoveBox(index) {
    $('#Box_' + index).remove();
}

var bankLists = [];

$.ajax({
    url: "/admin/bank/GetBanksList",
    method: "GET",
    success: function (model) {
        $.map(model, function (val, i) {
            var bank = {};
            bank.id = val.id;
            bank.title = val.title;
            bankLists.push(bank);
        });
    }
});