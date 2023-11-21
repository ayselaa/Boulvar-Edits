const priceRanges = document.querySelectorAll('.js-price-range');
 
priceRanges.forEach(el => {
 
    const downPriceInput = el.closest('.filter-price').querySelector('.js-price-down');
    const upPriceInput = el.closest('.filter-price').querySelector('.js-price-up');
    const inputs = [downPriceInput, upPriceInput];

    const minFloorArea = parseFloat(downPriceInput.value.replace(/\s+/g, '')) || 0; // Min değeri al
    const maxFloorArea = parseFloat(upPriceInput.value.replace(/\s+/g, '')) || 100000; // Max değeri al

    downPriceInput.value = minFloorArea.toLocaleString();
    upPriceInput.value = maxFloorArea.toLocaleString();

    const maxPrice = maxFloorArea; // Slider için max değer

    noUiSlider.create(el, {
        range: {
            'min': minFloorArea,
            'max': maxPrice
        },
        behaviour: 'drag',
        connect: true,
        start: [minFloorArea, maxPrice],
        step: 1
    });

    el.noUiSlider.on('update', values => {
        let [downPrice, upPrice] = values;

        downPrice = Number(downPrice).toLocaleString();
        upPrice = Number(upPrice).toLocaleString();

        downPriceInput.value = downPrice;
        upPriceInput.value = upPrice;
 

    });

    inputs.forEach(function (input, handle) {
        input.addEventListener('change', function () {
            debugger
            let value = this.value;
            value = value.replace(/\s+/g, '');
            value = parseFloat(value) || 0;

            el.noUiSlider.setHandle(handle, value);
        });
    });
});

const clearBtn = document.querySelector('.js-clear-sliders');

clearBtn.addEventListener('click', () => {
    debugger
    const filterPrices = document.querySelectorAll('.filter-price');

    filterPrices.forEach(el => {
        const priceRange = el.querySelector('.js-price-range');
        const priceRangeInputs = el.querySelectorAll('.filter-price__flex-row input');

        priceRangeInputs.forEach(function (input, handle) {
            const maxPrice = (handle) ? input.getAttribute('data-max') : 0;

            priceRange.noUiSlider.setHandle(handle, maxPrice);
        });
    });
});








 