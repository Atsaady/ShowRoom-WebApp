

$(document).ready(function () {

    var checkedArrLength = 0;
    var checkedArr = [];
    

    //Slider range
    $("#slider-range").slider({
        range: true,
        min: 0,
        max: 300,
        values: [1, 300],
        slide: function (event, ui) {
            console.log(ui.values[0], ui.values[1]);

            $("#amount").val("₪" + ui.values[0] + " - ₪" + ui.values[1]);         
        }
    });
    $("#amount").val("₪" + $("#slider-range").slider("values", 0) + " - ₪" + $("#slider-range").slider("values", 1));

    $("#category_filter_submit_btn").click(function () {
        var categories = [];
        $("input:checkbox[name=category]:checked").each(function () {
            categories.push($(this).val());
        });

        $.ajax({
            url: "/Clothings/FilterByCategory",
            dataType: "json",
            type: "POST",
            data: {
                category: categories
            },
            success: function (data) {
                console.log(data);
                $('#results').tmpl(data).appendTo('#tbody');
            },

        });
    });

    $("#submit_btn").click(function () {

        var min = $("#slider-range").slider("values", 0);
        console.log(min);
        var max = $("#slider-range").slider("values", 1);
        console.log(max);

       
        //If checkboxes are checked
        
        console.log(checkedArrLength);
        if (checkedArrLength > 0) {

            checkedArr.forEach(function (value) {
                var id = value;
                console.log(id);
                $.ajax({
                    url: "/Clothings/FilterByCategoryAndPrice",
                    data: {
                        id,
                        min: min,
                        max: max
                    },
                    success: function (data) {
                        console.log(data);
                        $('#results').tmpl(data).appendTo('#tbody');
                    },

                });
            });
        }
        
        else {

            var data =
            {
                min: min,
                max: max
            }
            console.log(data);
            var form = $('#sliderform');
            var url = form.attr('action');
            $.ajax({
                url: url,
                data: data,
                success: function (data) {
                    $('#results').tmpl(data).appendTo('#tbody');
                },

            });
        }
    });
   //End of Slider range



   //Checkboxes
   
   $('.category_checkbox').change(function checkbox() {

     var min = $("#slider-range").slider("values", 0);
     var max = $("#slider-range").slider("values", 1);

     //initialize checked array
      checkedArr = [];

     $.each($("input[type='checkbox']:checked"), function () {
         checkedArr.push(this.getAttribute("name"));
         
     });

     //set the current array length to the var outside from the scope
     checkedArrLength = checkedArr.length;

     checkedArr.forEach(function (value) {
         var id = value;
         console.log(id);
         $.ajax({
             url: "/Clothings/FilterByCategoryAndPrice",
             data: {
                 id,
                 min: min,
                 max: max
             },
             success: function (data) {
                 console.log(data);
                 $('#results').tmpl(data).appendTo('#tbody');
             },

         });
     });
   });
    //End Of Checkbox

    //Loader Spinner
    $('#loader').hide();
    $(document).ajaxStart(function () {
        $('#tbody').empty();
        $('#loader').show();
    }).ajaxStop(function () {
        $('#loader').hide();
    });
});






