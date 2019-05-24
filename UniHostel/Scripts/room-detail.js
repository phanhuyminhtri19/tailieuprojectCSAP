
(function($){
	$(document).ready(function($){
		"use strict"
		sliderHandler();
	});



	function sliderHandler(){
		if($('#sliderBig').length > 0){
			var sliderBig = $('#sliderBig ul').bxSlider({
				auto: true,
				mode: 'fade',
				speed: 200,
				pager: false,
				controls: true,
				responsive: true,	
				nextText: '<i class="fa fa-angle-right"></i>',
				prevText: '<i class="fa fa-angle-left"></i>',
				adaptiveHeight: true,
				infiniteLoop:false,
				hideControlOnEnd: true,
				onSlideBefore: function($slideElement, oldIndex, newIndex){
					changeThumb(sliderThumb,newIndex);
				}
			});

			var sliderThumb = $('#sliderThumb ul').bxSlider({
				minSlides: 4,
				maxSlides: 4,
				slideWidth: 156,
				slideMargin: 12,
				moveSlides: 1,
				pager:false,
				speed:1000,
				infiniteLoop:false,
				hideControlOnEnd:true,
				controls: false,
				onSlideBefore:function($slideElement, oldIndex, newIndex){}
			});


			linkRealSliders(sliderBig,sliderThumb);

			// if($('#sliderThumb li').length < 5) {
			// 	$('#sliderThumb .bx-next').hide();
			// }
		}
	}

	function linkRealSliders(bigS,thumbS){
		$("#sliderThumb ul").on("click","a",function(event){
			event.preventDefault();
			var newIndex = $(this).parent().attr("slideIndex");
	        bigS.goToSlide(newIndex);
	        
		});
	}

	function changeThumb(slider,newIndex){
		var $thumbS=$("#sliderThumb");
		$thumbS.find('.active').removeClass("active");
		$thumbS.find('li[slideIndex="'+newIndex+'"]').addClass("active");
		
		if(slider.getSlideCount()-newIndex>=4){
			slider.goToSlide(newIndex);
		}else {
			slider.goToSlide(slider.getSlideCount()-4);
		}
	}

})(jQuery);
	
