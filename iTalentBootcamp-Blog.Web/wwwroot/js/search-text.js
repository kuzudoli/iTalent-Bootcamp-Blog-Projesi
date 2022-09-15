$("#searchInput").keyup(function () {
    var searchText = $("#searchInput").val();
    var searchResultElement = document.getElementById("searchResult");
    searchResultElement.classList.add("visible");

    $.ajax({
        url: 'https://localhost:7241/api/Posts/GetPostsBySearch/' + searchText,
        type: 'get',
        success: function (result) {
            var resultList = $("#searchResult");

            resultList.empty();

            let rawPosts = result.data;

            rawPosts.forEach(function (item) {
                console.log(item);
                let li = document.createElement("li");
                let classList = ["bg-white", "mb-2", "px-3"];

                classList.forEach((item) => {
                    li.classList.add(item);
                });

                li.innerHTML = `<a class="searchResult-item" href="/Posts/${item.id}">${item.title}</a>`;
                searchResultElement.appendChild(li);
            });
        },
        error: function () {
            searchResultElement.classList.remove("visible");
        }
    })
});