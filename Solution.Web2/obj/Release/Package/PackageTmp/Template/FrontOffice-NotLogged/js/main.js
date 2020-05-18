(function($) {

	'use strict';

	$(document).ready(function(){

		// SUBMENU ARROW
		$('.site-navigation li:has(ul)').addClass('has-child');

		// POPUP GALLERY
		$('.popup-gallery').magnificPopup({
		  delegate: 'a', // child items selector, by clicking on it popup will open
		  type: 'image',
		  gallery:{
		    enabled:true
		  }
		});

		// TESTIMONIAL PAGE
		$('#masonry').masonry({
			columnWidth: 585,
			itemSelector:'.tbox'
		});

	});

	$(window).height(function(){
		$('.onscreen, .slides li').css('height', window.innerHeight - 84);
	});

	$(window).load(function(){

		// FLEXSLIDER METHOD
		$('.flexslider').flexslider({
		    animation: "fade",
		    controlNav: false
		});

		$('.service-flexslider').flexslider({
		    animation: "fade",
		    controlNav: "thumbnails"
		});

		// MENU ON SCROLL
		$(window).scroll(function () {
			var $this = $(this);
			if ($this.scrollTop() > 240) {
				$('body').addClass('on-scroll');
			} else if($this.scrollTop() < 240){
				$('body').removeClass('on-scroll');
			}
			delete $this.this;
		});
	});

})( jQuery );
