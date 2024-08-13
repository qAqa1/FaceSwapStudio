class SingleImagesProcessingResultUiController {
    static #processing = new SingleImagesProcessingResultApi();

    static getAll() {
        SingleImagesProcessingResultUiController.#processing.getAll()
            .then(response => response.json())
            .then(data => SingleImagesProcessingResultUiController.#displayItems(data))
            .catch(error => console.error('Unable to get items.', error));
    }

    static #displayItems(dataItems) {
        dataItems.forEach(item => {
            console.log(item)
            $('#processing-result-list').find('tbody').append(React.renderToString(
                React.createElement('tr', {},
                    React.createElement('th', {scope: 'row'}, `${item.id}`),
                    React.createElement('td', {}, `${item.startCalculationDateTime}`),
                    React.createElement('td', {className: 'col-lg-4'}, `${item.endCalculationDateTime}`),
                    React.createElement('td', {className: 'col-lg-4'}, `${item.bodyImage}`),
                    React.createElement('td', {className: 'col-lg-4'}, `${item.faceImage}`),
                    React.createElement('td', {className: 'col-lg-4'}, `${item.swapImage}`),
                    React.createElement('td', {className: 'col-lg-4'}, `${item.enchancedSwapImage}`),
                )))
        })
    }
}

SingleImagesProcessingResultUiController.getAll()