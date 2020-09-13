function AddPromoCode() {
    const grid = document.querySelector('.promo-browse');
    const percent = document.getElementById('txtPercent').value;
    const name = document.getElementById('txtName').value;
    const count = document.getElementById('txtCount').value;

    if (name == null || name == '') {
        UIkit.notification("لطفا نام کد تخفیف را وارد نمایید");
        return;
    }
    if (count == null || count == '') {
        UIkit.notification("تعداد کد تخفیف را وارد نمایید");
        return;
    }
    if (percent == null || percent == '') {
        UIkit.notification("لطفا درصد تخفیف را وارد نمایید");
        return;
    }

    if (parseInt(percent) >= 100) {
        UIkit.notification("درصد باید کمتر از 100 باشد");
        return;
    }

    if (parseInt(percent) <= 0) {
        UIkit.notification("درصد باید بیشتر از 0 باشد");
        return;
    }
    if (parseInt(count) <= 0) {
        UIkit.notification("تعداد باید بیشتر از 0 باشد");
        return;
    }
    grid.innerHTML +=
        `
        <!-- #region promo -->

        <div class="promo-code row">
            <a class="uk-icon uk-margin-left" 
                onclick="RemovePromoCode(this)" 
                uk-icon="icon: trash"></a>
            <label class="">${name}</label>
            <label class="uk-badge uk-margin-auto-right">${percent}%</label>
        </div>

        <!-- #endregion -->
        `

    var Name = document.getElementById('txtName').value;
    var Discount = document.getElementById('txtPercent').value;
    var Count = document.getElementById('txtCount').value;


    document.getElementById('txtPercent').value = '';
    document.getElementById('txtName').value = '';
    document.getElementById('txtCount').value = '';

    AddDiscount(Name, Discount, Count);
}

function RemovePromoCode(itemId) {
    const item = document.getElementById(itemId);
    console.log(item);
    item.parentNode.removeChild(item);
    DeleteDiscount(itemId);
}