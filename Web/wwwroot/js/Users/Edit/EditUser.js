$('#add-new-familyRole').click(function () {
    var index = document.getElementsByName('familyBox').length;

    var newBox = `<div class="card" id="Box_` + index + `" style="background-color: #ffffff">
        <div class="card-header" name="familyBox"><h6>#`+ (index + 1) + ` رابطه خویشاوندی</h6></div>
        <div class="card-body">
            <div class="row">
                <input name="AdditionalUserData[`+ index + `].ParentRef" value="` + $('#Id').val() + `" hidden="" type="text" id="AdditionalUserData_0__ParentRef">
                <div class="col-6 mb-2">
                    <label for="AdditionalUserData[`+ index + `].FirstName">نام</label>
                    <input type="hidden" maxlength="26" name="AdditionalUserData.index" value="`+ index + `" />
                    <input name="AdditionalUserData[`+ index + `].FirstName" class="form-control" type="text" id="AdditionalUserData_0__FirstName" value="" style="background-color: #ffffff!important;color:black;border:1px solid #ced4da">
                </div>
                <div class="col-6 mb-2">
                    <label for="AdditionalUserData[`+ index + `].LastName">نام خانوادگی</label>
                    <input name="AdditionalUserData[`+ index + `].LastName" class="form-control" type="text" id="AdditionalUserData_0__LastName" value="" style="background-color: #ffffff!important;color:black;border:1px solid #ced4da">
                </div>
                <div class="col-6 mb-2">
                    <label for="AdditionalUserData[`+ index + `].NationalCode"> کد ملی</label>
                    <input name="AdditionalUserData[`+ index + `].NationalCode" class="form-control" type="text" id="AdditionalUserData_0__NationalCode" value="" style="background-color: #ffffff!important;color:black;border:1px solid #ced4da">
                </div>
                <div class="col-6 mb-2">
                    <label for="AdditionalUserData[`+ index + `].IdentityNumber"> شماره شناسنامه</label>
                    <input name="AdditionalUserData[`+ index + `].IdentityNumber" class="form-control" type="text" id="AdditionalUserData_0__IdentityNumber" value="" style="background-color: #ffffff!important;color:black;border:1px solid #ced4da">
                </div>
                <div class="col-6 mb-2">
                    <label for="AdditionalUserData[`+ index + `].Birthday">تاریخ تولد</label>
                    <input name="AdditionalUserData[`+ index + `].Birthday" id="AdditionalUserData_` + index + `__Birthday" class="datepicker form-control">
                </div>
                <div class="col-6 mb-2">
                    <label class="form-label" for="WorkshopCode">جنسیت</label>
                    <select class="form-control" id="AdditionalUserData_0__Gender" name="AdditionalUserData[`+ index + `].Gender" style="background-color: #ffffff!important;color:black;border:1px solid #ced4da">
                        <option value="0">زن</option>
                        <option value="1">مرد</option>
                    </select>
                </div>
                <div class="col-6 mb-2">
                    <label class="form-label" for="WorkshopCode">رابطه خویشاوندی</label>
                    <select class="form-control" data-val="true" data-val-required="The FamilyRole field is required." id="AdditionalUserData_0__FamilyRole" name="AdditionalUserData[`+ index + `].FamilyRole" style="background-color: #ffffff!important;color:black;border:1px solid #ced4da">
                        <option value="1">همسر</option>
                        <option value="2">دختر</option>
                        <option value="3">پسر</option>
                        <option value="4">مادر</option>
                        <option value="5">پدر</option>
                    </select>
                </div>
            </div>
        </div>
        <div class="card-footer text-right">
            <button type="button" onclick="RemoveBox('`+ index + `')" class="btn btn-danger">
                <i>حذف</i>
            </button>
        </div>
    </div>`

    $('#mainBox').after(newBox);
    $('#AdditionalUserData_' + index + '__Birthday').persianDatepicker();
});

function RemoveBox(index) {
    $('#Box_' + index).remove();
}