//write the movie table with movies from the list
function MovieListToHtml(moviesList) {
	var moviesTable = '<div id="bodyContainer" class="container" style="background-color: #ffffff;"><table class="table"><thead><tr><th>Title</th><th>Genere</th><th>UnitsInStock</th><th>ReleaseDate</th><th>Actors</th><th>MinAge</th><th>Language</th><th>UnitPrice</th><th></th></thead><tbody>';
    $.each(moviesList, function (index, movie) {
                            moviesTable = moviesTable.concat('<tr><td>' + movie.Title + '</td>' + '<td>' + movie.Genere + '</td>' + '<td>' + movie.UnitsInStock + '</td>' + '<td>' + movie.ReleaseDate + '</td>' + '<td>' + movie.Actors + '</td>' + '<td>' + movie.MinAge + '</td>' + '<td>' + movie.Language + '</td>' + '<td>' + movie.UnitPrice.Wholesale + '</td></tr>');
                       });
        moviesTable = moviesTable.concat('</tbody></table></div>');
    $(bodyContainer).replaceWith(moviesTable);
}
// regular shearch (with the Title)
function Search() {
    var search = $("#searchTxt").val();
    AjaxMovie('AdvancedSearch', MovieListToHtml, { "Title": search });
}
//write the AdvancedSearchPage
function AdvancedSearchPage() {
    var SearchPage = '<div id="bodyContainer" class="container" style="background-color: #ffffff;">';
    SearchPage = SearchPage.concat('<h2>Advance Search Page</h2>')
    SearchPage = SearchPage.concat('<form class="form-inline md-form form-sm active-cyan-2"><input id="Title" class="form-control form-control-sm mr-3 w-75" type="text" placeholder="Title" aria-label="Search"></form>');
    SearchPage = SearchPage.concat('<form class="form-inline md-form form-sm active-cyan-2"><input id="Actors" class="form-control form-control-sm mr-3 w-75" type="text" placeholder="Actor1,Actor2,...." aria-label="Search"></form>');
    SearchPage = SearchPage.concat('<form class="form-inline md-form form-sm active-cyan-2"><input id="Year" class="form-control form-control-sm mr-3 w-75" type="text" placeholder="Year" aria-label="Search"></form>');
    SearchPage = SearchPage.concat('<br/><button id="multiple-search-btn" type="submit" width="60" height="100" onclick="AdvancedSearch()">GO!</button>');
    SearchPage = SearchPage.concat('</div>');
    $(bodyContainer).replaceWith(SearchPage);
}
//AdvancedSearch with 3 parameters
function AdvancedSearch() {
    var title = $("#Title").val();
    var actors = $("#Actors").val();
    var year = $("#Year").val();
    AjaxMovie('AdvancedSearch', MovieListToHtml, { "Title": title, "Actors": actors, "year": year })
}
//Ajax for all movie table
function loadTableMovies() {
    AjaxMovie('AdvancedSearch', MovieListToHtml, {} );
}
// Ajax function for call functions from Movies , func for success and dictionary for send to same function
function AjaxMovie(controllerFunc, changeFunc, dictionaryDataForController) {
    $.ajax({
        'type': 'POST',
        'dataType': 'json',
        'url': '/Movies/' + controllerFunc,
        'data': dictionaryDataForController,
        'success': function (movies) {
            changeFunc(movies);
        },
        'error': function () {
            alert("Error while inserting data");
        }
    });
}