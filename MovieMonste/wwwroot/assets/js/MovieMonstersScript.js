//write the Titles of table
function headTable(titlesList) {
    var head = '<thead><tr>'
    $.each(titlesList, function (index, title) {
        head = head.concat('<th>' + title + '</th>');
    });
    head = head.concat('</tr></thead>');
    return head;
}

//write the movie table with movies from the list
function MovieListToHtml(moviesList) {
    var moviesTable = '<table class="table">';
    moviesTable = moviesTable.concat(headTable(["Title", "Genere", "UnitsInStock", "ReleaseDate", "Actors", "MinAge", "Language","UnitPrice"] ));
    $.each(moviesList, function (index, movie) {
                            moviesTable = moviesTable.concat('<tr><td>' + movie.Title + '</td>' + '<td>' + movie.Genere + '</td>' + '<td>' + movie.UnitsInStock + '</td>' + '<td>' + movie.ReleaseDate + '</td>' + '<td>' + movie.Actors + '</td>' + '<td>' + movie.MinAge + '</td>' + '<td>' + movie.Language + '</td>' + '<td>' + movie.UnitPrice.Wholesale + '</td></tr>');
                       });
    moviesTable = moviesTable.concat('</tbody></table>');
    ChangeBodyContainerView(moviesTable, '/Movies')
}
// regular shearch (with the Title)
function Search() {
    $("#searchTxt").val("muli maniak");
    var search = $("#searchTxt").val();
    AjaxMovie('AdvancedSearch',MovieListToHtml, { "Title": search });
}
//write the AdvancedSearchPage
function AdvancedSearchPage() {
    var SearchPage = '<h2>Advance Search Page</h2>';
    SearchPage = SearchPage.concat('<form class="form-inline md-form form-sm active-cyan-2"><input id="Title" class="form-control form-control-sm mr-3 w-75" type="text" placeholder="Title" aria-label="Search"></form>');
    SearchPage = SearchPage.concat('<form class="form-inline md-form form-sm active-cyan-2"><input id="Actors" class="form-control form-control-sm mr-3 w-75" type="text" placeholder="Actor1,Actor2,...." aria-label="Search"></form>');
    SearchPage = SearchPage.concat('<form class="form-inline md-form form-sm active-cyan-2"><input id="Year" class="form-control form-control-sm mr-3 w-75" type="text" placeholder="Year" aria-label="Search"></form>');
    SearchPage = SearchPage.concat('<br/><button id="multiple-search-btn" type="submit" width="60" height="100" onclick="AdvancedSearch()">GO!</button>');
    ChangeBodyContainerView(SearchPage, '/Home');
}
//AdvancedSearch with 3 parameters
function AdvancedSearch() {
    var title = $("#Title").val();
    var actors = $("#Actors").val();
    var year = $("#Year").val();
    AjaxMovie('AdvancedSearch', MovieListToHtml, { "Title": title, "Actors": actors, "year": year });
}
//Ajax for all movie table
function loadTableMovies() {
    AjaxMovie('ShortIndex', function (htmlIndex) {
        ChangeBodyContainerView(htmlIndex, '/Movies');
    }, { }, 'html' );
}
// Change the general div to any html and change the url with out refresh
function ChangeBodyContainerView (stringHtml, updateURL) {
    $(bodyContainer).html(stringHtml);
    if (updateURL!=null)
        history.pushState(null, '', updateURL);
}
// Ajax function for call functions from Movies , func for success and dictionary for send to same function
function AjaxMovie(controllerFunc, changeFunc, dictionaryDataForController, dataType='json') {
    $.ajax({
        'type': 'POST',
        'dataType': dataType,
        'url': '/Movies/' + controllerFunc,
        'data': dictionaryDataForController,
        'success': function (data) {
            changeFunc(data);
        },
        'error': function () {
            alert("Error while inserting data");
        }
    });
}

function loginPage() {
    var SearchPage = '<h2>LogIn</h2>';
    SearchPage = SearchPage.concat('<form class="form-inline md-form form-sm <form class="form" action="/login">');
    SearchPage = SearchPage.concat('<label for="email">Email: </label><input class="form-control" id="email" name="email" autofocus required>');
    SearchPage = SearchPage.concat('<label for="password">Password: </label><input class="form-control" id="password" name="password" required>');
    SearchPage = SearchPage.concat('<br/><input class="form-control btn btn-lg btn-primary" type="submit"></form>');
    ChangeBodyContainerView(SearchPage, '/Home');
}