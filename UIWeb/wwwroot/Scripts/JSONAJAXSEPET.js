function JSSepeteEkle(ProductIds) {
    $.ajax({
        type: "POST",
        url: "/Category/SepeteEkle",
        data: {Id: '' + ProductIds + '' },
        dataType: "json",
        success: function (responseS) {
            $("#BasketControl").load("/PartialView/BasketQuantityControl"),
            alert(responseS);
        },
        failure: function (responseS) {
        },
        error: function (responseS) {
        }
    });
};

function JSDeleteBasket(ProductIds) {
    $.ajax({
        type: "POST",
        url: "/Basket/DeleteBasket",
        data: { Id: '' + ProductIds + '' },
        dataType: "json",
        success: function (responseS) {
            $("#BasketControl").load("/PartialView/BasketQuantityControl");
            $("#BasketDiv").load("/Basket/GetPartialBasket");
        },
        failure: function (responseS) {
        },
        error: function (responseS) {
        }
    });
};

    function JSVaryantSepeteEkle(ProductIds) {

        var allVariants = $("input[type = 'radio']");
        var variantGroups = [];
        
        $(allVariants).each(function ( i, item) {
            var groupName = $(item).attr("name");
            if (!variantGroups.includes(groupName)) {
                variantGroups.push(groupName);
            }
        });

        var secilenVariantlar = [];
        $(variantGroups).each(function (i, item) {
            var variantId = $("input[type = 'radio'][name ='" + item + "']:checked").val();
            secilenVariantlar.push(variantId);
        });

        var GelenBeden = $("input[type = 'radio'][name ='VariantSize']:checked").val();
        var GelenRenk = $("input[type = 'radio'][name ='VariantColor']:checked").val();
     
        $.ajax({
            type: "POST",
            url: "/Basket/AddVariantsToBasket",
            data: {Id: ProductIds,variantIds: secilenVariantlar},
            dataType: "json",
            success: function (responseS) {
                $("#BasketControl").load("/PartialView/BasketQuantityControl"),
                    alert(responseS);
            },
            failure: function (responseS) {
            },
            error: function (responseS) {
            }
        });

    };


