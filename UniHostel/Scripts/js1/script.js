$(document).ready(function(){
	"use strict";

    // Preloader
    $(window).on('load', function() {
        $('#preloader').addClass('zoomOut');
    });

	/* Responsive menu*/
	if($("#mp-menu").length > 0)
	new mlPushMenu(document.getElementById( 'mp-menu' ), document.getElementById( 'sys_btn_toogle_menu' ),{dockSide: "right"} ); 

	// Heading large	
	var doMargintop = -$(".decription-override").outerHeight()/2;
	$(".decription-override").css("margin-top",doMargintop);
	
	//=============== IF IE 8 ===================
	var rex = new RegExp("MSIE 8.0");
	var trueIE = rex.test(navigator.userAgent);
	
	if(trueIE) {
		jQuery('#mp-menu').hide();
		// jQuery('.md-slide').find('LI').children('IMG').css({
		// 	top: '0px',
		// 	left: '0px'
		// });
		jQuery('.content-slide .home-content').css({
			left: '24%',
			top: '25%'
		});
	}

	/*IF IE 9*/
    var rex = new RegExp("MSIE 9.0");
    var trueIE = rex.test(navigator.userAgent);

    var flag_show = false;  
        if(trueIE) {

            
            $(".mp-menu").hide();
            $(".has-sub .mp-level").hide();
            //  Show menu
            $(".btn-toogle-res-menu").click(function(ev) {
                ev.stopPropagation();
                $(".mp-menu").toggle("drop",{direction: "right"},300);          
            });
             // Show sub-menu
            $(".has-sub").click(function(event){
                    event.stopPropagation();
                    $(this).children(".mp-level").show("slide",{direction: "right"},300);
                })
            // Close sub-menu
            $('.mp-back').click(function(){
                $(this).parent().hide("slide",{direction: "right"},300);
            })
            // 
            $('.mp-menu').click(function(event){
                     event.stopPropagation();
            })
            
    }
     /*END IE 9*/
    if ($('#quote-slider').length) {
        $('#quote-slider').bxSlider({
            auto: true,
            mode: 'fade',
            speed: 100,
            pause: 6000,
            pager: false,
            controls: false,
            responsive: true

        });
    }


});

