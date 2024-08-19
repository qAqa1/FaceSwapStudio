class SingleImagesProcessingResultUiController {
    static #getItemsContainer() {
        return $('#processing-result-list').find('tbody')
    }

    static getAll(scrollToEnd = true) {
        SingleImagesProcessingResultApi.getAll()
            .then(response => response.json())
            .then(data => {
                SingleImagesProcessingResultUiController.#displayItems(data, scrollToEnd)
            })
            .catch(error => console.error('Unable to get items.', error));
    }

    static #displayItems(dataItems, scrollToEnd) {
        let itemsContainer = SingleImagesProcessingResultUiController.#getItemsContainer()
        itemsContainer.html('')

        dataItems.forEach(item => {
            const tableCellClass = 'col-lg-4'

            let rowRoot = $("<tr>");

            $("<th>", {"class": tableCellClass}).html(`${item.id}`).appendTo(rowRoot)

            let separator = "-";
            let endCalculationDateTime = new Date(Date.parse(item.endCalculationDateTime));
            let endDateString =
                endCalculationDateTime.getFullYear() + separator +
                ("0" + (endCalculationDateTime.getMonth() + 1)).slice(-2) + separator +
                ("0" + endCalculationDateTime.getDate()).slice(-2) + " " +
                ("0" + endCalculationDateTime.getHours()).slice(-2) + separator +
                ("0" + endCalculationDateTime.getMinutes()).slice(-2) + separator +
                ("0" + endCalculationDateTime.getSeconds()).slice(-2);

            $("<td>", {"class": tableCellClass}).html(endDateString).appendTo(rowRoot)
            if (item.faceImage != null && item.faceImage !== '') {
                $("<td>", {"class": tableCellClass}).append($("<img>", {
                    'width': 300,
                    'src': "data:image/png;base64," + item.faceImage
                })).appendTo(rowRoot)
            } else
                $("<th>", {"class": tableCellClass}).appendTo(rowRoot)

            if (item.bodyImage != null && item.bodyImage !== '') {
                $("<td>", {"class": tableCellClass}).append($("<img>", {
                    'width': 300,
                    'src': "data:image/png;base64," + item.bodyImage
                })).appendTo(rowRoot)
            } else
                $("<th>", {"class": tableCellClass}).appendTo(rowRoot)

            if (item.enchancedSwapImage != null && item.enchancedSwapImage !== '') {
                $("<td>", {"class": tableCellClass}).append($("<img>", {
                    'width': 600,
                    'src': "data:image/png;base64," + item.enchancedSwapImage
                })).appendTo(rowRoot)
            } else
                $("<th>", {"class": tableCellClass}).appendTo(rowRoot)

            let deleteDiv = $("<div>", {}).append($("<button>", {"class": 'btn btn-secondary my-2'}).html('delete').click(function () {
                if (confirm('Are you sure you want to delete this swap result?'))
                    SingleImagesProcessingResultUiController.delete(item.id)
            }))

            $("<td>", {"class": tableCellClass}).append(deleteDiv).appendTo(rowRoot)

            itemsContainer.append(rowRoot).ready(function () {
                if (scrollToEnd)
                    window.scrollTo(0, document.body.scrollHeight)
            })
        })
    }

    static delete(id) {
        SingleImagesProcessingResultUiController.#getItemsContainer().html("")
        SingleImagesProcessingResultApi.delete(id).then(() => SingleImagesProcessingResultUiController.getAll())
    }
}

SingleImagesProcessingResultUiController.getAll()