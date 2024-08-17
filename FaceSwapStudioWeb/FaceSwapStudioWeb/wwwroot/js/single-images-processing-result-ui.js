class SingleImagesProcessingResultUiController {
    static #getItemsContainer() {
        return $('#processing-result-list').find('tbody')
    }

    static getAll(scrollToEnd = true) {
        SingleImagesProcessingResultApi.getAll()
            .then(response => response.json())
            .then(data => {
                SingleImagesProcessingResultUiController.#displayItems(data, scrollToEnd)
                // $.when(SingleImagesProcessingResultUiController.#displayItems(data))
                //     .then(window.scrollTo(0, document.body.scrollHeight))
            })
            .catch(error => console.error('Unable to get items.', error));
    }

    static #displayItems(dataItems, scrollToEnd) {
        // dataItems.forEach(item => {
        //     // console.log(item)
        //     $('#processing-result-list').find('tbody').append(React.renderToString(
        //         React.createElement('tr', {},
        //             React.createElement('th', {scope: 'row'}, `${item.id}`),
        //             React.createElement('td', {}, `${item.startCalculationDateTime}`),
        //             React.createElement('td', {className: 'col-lg-4'}, `${item.endCalculationDateTime}`),
        //             React.createElement('td', {className: 'col-lg-4'}, `${item.bodyImage}`),
        //             React.createElement('td', {className: 'col-lg-4'}, `${item.faceImage}`),
        //             React.createElement('td', {className: 'col-lg-4'}, `${item.swapImage}`),
        //             React.createElement('td', {className: 'col-lg-4'}, `${item.enchancedSwapImage}`),
        //             React.createElement('td', {className: 'col-lg-4'},
        //                 React.createElement('img', {src: 'https://avatars.mds.yandex.net/i?id=766637e7fecd215c2916b5d4741bd5f4_l-5282144-images-thumbs&n=27&h=480&w=480'}),
        //             )
        //         )))
        // })

        let itemsContainer = SingleImagesProcessingResultUiController.#getItemsContainer()
        itemsContainer.html('')

        dataItems.forEach(item => {
            // itemsContainer.html("")

            // var input = React.renderToString(React.createElement("button", {onClick: () => {console.log('ad')}}, ""));
            // alert(input)

            // let html = React.renderToString(
            //     React.createElement('tr', {},
            //         React.createElement('th', {scope: 'row'}, `${item.id}`),
            //         React.createElement('td', {className: 'col-lg-4'}, `${item.endCalculationDateTime}`),
            //         React.createElement('td', {className: 'col-lg-4'},
            //             React.createElement('img', {src: 'https://avatars.mds.yandex.net/i?id=766637e7fecd215c2916b5d4741bd5f4_l-5282144-images-thumbs&n=27&h=480&w=480'}),
            //         ),
            //         // React.createElement('td', {className: 'col-lg-4'},
            //         //     React.createElement('div', {},
            //         //         React.createElement('button', {className: "btn btn-primary my-2"}, 'details'),
            //         //         React.createElement('button', {className: "btn btn-secondary my-2"}, 'remove'),
            //         //     )),
            //         React.createElement('td', {className: 'col-lg-4'},
            //             React.createElement('div', {},
            //                 React.createElement('button', {className: "btn btn-primary my-2"}, 'details')),
            //             React.createElement('div', {},
            //                 React.createElement('button',
            //                     {
            //                         className: "btn btn-secondary my-2",
            //                         id: `remove-item-${item.id}`,
            //                     }
            //                 , 'remove')),
            //         )
            //     ))

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
            // $("<td>", {"class": tableCellClass}).append($("<img>", {'src': 'https://avatars.mds.yandex.net/i?id=766637e7fecd215c2916b5d4741bd5f4_l-5282144-images-thumbs&n=27&h=480&w=480'})).appendTo(rowRoot)

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

            // let detailsDiv = $("<div>", {}).append($("<button>", {"class": 'btn btn-primary my-2'}).html('details').click(function () {
            //     alert(`${item.id}`)
            // }))

            let deleteDiv = $("<div>", {}).append($("<button>", {"class": 'btn btn-secondary my-2'}).html('delete').click(function () {
                // alert(`${item.id}`)

                if (confirm('Are you sure you want to delete this swap result?'))
                    SingleImagesProcessingResultUiController.delete(item.id)

                // SingleImagesProcessingResultUiController.delete(item.id)
            }))

            $("<td>", {"class": tableCellClass})./*append(detailsDiv).*/append(deleteDiv).appendTo(rowRoot)

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