$(document).ready(function () {
    function close_accordion_section() { //akordiyon bölümlerini kapat
        $('.accordion .accordion-section-title').removeClass('active'); //active class sil
        $('.accordion .accordion-section-content').slideUp(300).removeClass('open'); //sayfayi yukari dogru kapa yavasca kapat, open class sil
    }
    $('.accordion-section-title').click(function (e) {
        if ($(e.target).is('.active')) { //tiklanilan akordiyon sayfasi acik ise
            //close_accordion_section(); //kapat sayfayi
        } else {
            close_accordion_section(); //kapat sayfayi
            $(this).addClass('active'); //tiklanilan akordiyonu aktif et
            $(this).next().slideDown(300).addClass('open'); //tiklanan akordiyon icerigini asagi dogru yavasca ac ve open class ata
        }
        e.preventDefault(); //a href sayfa yonlendirme kapat
    });
});
function gonder(task_id) {
    bootbox.confirm("Are you sure want to send?", function (result) {
        if (result) {
            $.ajax({
                url: '/Task/gonder',
                data: { id: task_id },
                type: 'POST',
                success: function (cevap) {
                    bootbox.alert("It was sent to the manager.");
                    location.reload();
                }
            })
        }
    });
}
function onayla(task_id) {
    bootbox.confirm("Are you sure want to approve?", function (result) {
        if (result) {
            $.ajax({
                url: '/Task/onayla',
                data: { id: task_id },
                type: 'POST',
                success: function (cevap) {
                    bootbox.alert("It was sent to the manager.");
                    location.reload();
                }
            })
        }
    });
}

function reddet(task_id) {
    bootbox.confirm("Are you sure want to decline?", function (result) {
        if (result) {
            $.ajax({
                url: '/Task/reddet',
                data: { id: task_id },
                type: 'POST',
                success: function (cevap) {
                    bootbox.alert("It was declined.");
                    location.reload();
                }
            })
        }
    });
}


function kurtar(proje_id) {
    bootbox.confirm("Are you sure want to recover?", function (result) {
        if (result) {
            $.ajax({
                url: '/Admin/kurtar',
                data: { id: proje_id },
                type: 'POST',
                success: function (cevap) {
                    bootbox.alert("It was recovered.");
                    location.reload();
                }
            })
        }
    });
}
function sil(proje_id) {
    bootbox.confirm("Are you sure want to delete?", function (result) {
        if (result) {
            $.ajax({
                url: '/Proje/sil',
                data: { id: proje_id },
                type: 'POST',
                success: function (cevap) {
                    bootbox.alert("It was deleted.");
                    location.reload();
                }
            })
        }
    });
}
