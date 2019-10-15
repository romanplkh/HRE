$(document).ready(function() {
	if ($('.owl-carousel').length > 0) {
		$('.owl-carousel').owlCarousel({
			loop: true,
			margin: 20,
			autoplay: true,
			autoplayTimeout: 4000,
			autoplayHoverPause: true,

			responsive: {
				0: {
					items: 1
				},
				600: {
					items: 2
				},
				1000: {
					items: 3
				},
				1200: {
					items: 4
				}
			}
		});

		if ($('.slider').length > 0) {
			$('.slider').slick({
				infinite: true,
				slideToShow: 1,
				slideToScroll: 1,
				autoplay: true,
				autoplaySpeed: 5000,
				speed: 1800
			});
		}
	}

	if ($('#sidebarCollapse').length > 0) {
		$('#sidebarCollapse').on('click', function() {
			$('#sidebar').toggleClass('active');
			if ($('#toggler-icon i').hasClass('fa-caret-right')) {
				$('#toggler-icon i').removeClass('fa-caret-right');
				$('#toggler-icon i').addClass('fa-caret-left');
			} else {
				$('#toggler-icon i').removeClass('fa-caret-left');
				$('#toggler-icon i').addClass('fa-caret-right');
			}
		});
    }


    if ($("#sidebar").length > 0) {
      
        if (window.innerWidth >= 1250 || document.documentElement.clientWidth >= 1250
            || document.body.clientWidth >= 1250) {
            $("#sidebar").removeClass("active");
        } else {
            $("#sidebar").addClass("active");
        }

        window.addEventListener("resize",function () {
            if (window.innerWidth >= 1250 || document.documentElement.clientWidth >= 1250
                || document.body.clientWidth >= 1250) {
                $("#sidebar").removeClass("active");
            } else {
                $("#sidebar").addClass("active");
            }
        });
    }
});
