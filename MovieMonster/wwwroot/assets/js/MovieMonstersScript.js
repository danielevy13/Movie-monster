///////////////GeneralScripts////////////////////////////

//write the Titles of table
function headTable(titlesList) {
    var head = '<thead><tr>'
    $.each(titlesList, function (index, title) {
        head = head.concat('<th>' + title + '</th>');
    });
    head = head.concat('</tr></thead>');
    return head;
}

// Ajax function for call functions from Movies , func for success and dictionary for send to same function
function ToAjax(controllerFunc, controller, changeFunc, dictionaryDataForController, dataType = 'json') {
    $.ajax({
        'type': 'POST',
        'dataType': dataType,
        'url': '/' + controller + '/' + controllerFunc,
        'data': dictionaryDataForController,
        'success': function (data) {
            changeFunc(data);
        },
        'error': function () {
            alert("Error while inserting data");
        }
    });
}

//Changes renderBody
function ChangeBodyContainerView(stringHtml, updateURL) {
    $(bodyContainer).html(stringHtml);
    if (updateURL != null)
        history.pushState(null, '', updateURL);
}

///////////////MoviesScripts////////////////////////////

// regular shearch (with the Title)
function Search() {
    var search = $("#searchTxt").val();
    ToAjax('AdvancedSearch','Movies',MovieListToHtml, { "Title": search });
}

//write the AdvancedSearchPageForMovies
function MoviesAdvancedSearchPage() {
    var SearchPage = '<h2>Advance Search Page</h2>';
    SearchPage = SearchPage.concat('<form class="form-inline md-form form-sm active-cyan-2"><input id="Title" class="form-control form-control-sm mr-3 w-75" type="text" placeholder="Title" aria-label="Search"></form>');
    SearchPage = SearchPage.concat('<form class="form-inline md-form form-sm active-cyan-2"><input id="Actors" class="form-control form-control-sm mr-3 w-75" type="text" placeholder="Actor1,Actor2,...." aria-label="Search"></form>');
    SearchPage = SearchPage.concat('<form class="form-inline md-form form-sm active-cyan-2"><input id="Year" class="form-control form-control-sm mr-3 w-75" type="text" placeholder="Year" aria-label="Search"></form>');
    SearchPage = SearchPage.concat('<br/><button id="multiple-search-btn" type="submit" width="60" height="100" onclick="MoviesAdvancedSearch()">GO!</button>');
    ChangeBodyContainerView(SearchPage, '/Home');
}

//MoviesAdvancedSearch with 3 parameters
function MoviesAdvancedSearch() {
    var title = $("#Title").val();
    var actors = $("#Actors").val();
    var year = $("#Year").val();
    ToAjax('AdvancedSearch', 'Movies', MovieListToHtml, { "Title": title, "Actors": actors, "year": year });
}

//write the movie table with movies from the list
function MovieListToHtml(moviesList) {
    var moviesTable = '<table class="table">';
    moviesTable = moviesTable.concat(headTable(["Title", "Genere", "UnitsInStock", "ReleaseDate", "Actors", "MinAge", "Language", "UnitPrice"]));
    $.each(moviesList, function (index, movie) {
        moviesTable = moviesTable.concat('<tr><td>' + movie.Title + '</td>' + '<td>' + movie.Genere + '</td>' + '<td>' + movie.UnitsInStock + '</td>' + '<td>' + movie.ReleaseDate + '</td>' + '<td>' + movie.Actors + '</td>' + '<td>' + movie.MinAge + '</td>' + '<td>' + movie.Language + '</td>' + '<td>' + movie.UnitPrice.Wholesale + '</td></tr>');
    });
    moviesTable = moviesTable.concat('</tbody></table>');
    ChangeBodyContainerView(moviesTable, '/Movies')
}

//Ajax for all movie table
function loadTableMovies() {
    ToAjax('ShortIndex','Movies', function (htmlIndex) {
        ChangeBodyContainerView(htmlIndex, '/Movies');
    }, { }, 'html' );
}

///////////////CustomersScripts////////////////////////////

//write the AdvancedSearchPageForCustomer
function CustomersAdvancedSearchPage() {
    var SearchPage = '<h2>Advance Search Page</h2>';
    SearchPage = SearchPage.concat('<form class="form-inline md-form form-sm active-cyan-2"><input id="CustomerID" class="form-control form-control-sm mr-3 w-75" type="text" placeholder="Email" aria-label="Search"></form>');
    SearchPage = SearchPage.concat('<form class="form-inline md-form form-sm active-cyan-2"><input id="BirthDate" class="form-control form-control-sm mr-3 w-75" type="text" placeholder="Year of birth" aria-label="Search"></form>');
    SearchPage = SearchPage.concat('<form class="form-inline md-form form-sm active-cyan-2"><input id="State" class="form-control form-control-sm mr-3 w-75" type="text" placeholder="State" aria-label="Search"></form>');
    SearchPage = SearchPage.concat('<form class="form-inline md-form form-sm active-cyan-2"><input id="City" class="form-control form-control-sm mr-3 w-75" type="text" placeholder="City" aria-label="Search"></form>');
    SearchPage = SearchPage.concat('<br/><button id="multiple-search-btn" type="submit" width="60" height="100" onclick="CustomersAdvancedSearch()">GO!</button>');
    ChangeBodyContainerView(SearchPage, '/Customers');
}

//CustomerAdvancedSearch with 4 parameters
function CustomersAdvancedSearch() {
    var email = $("#CustomerID").val();
    var year = $("#BirthDate").val();
    var state = $("#State").val();
    var city = $("#City").val();
    ToAjax('AdvancedSearch', 'Customers', CustomerListToHtml, { "CustomerID": email, "Year": year, "State": state, "City": city });
}

//Builds customer table after search
function CustomerListToHtml(customersList) {
    var customersTable = '<table class="table">';
    customersTable = customersTable.concat(headTable(["Email", "Full Name", "BirthDate", "PhoneNumber", "Gender", "State","City"]));
    $.each(customersList, function (index, customer) {
        customersTable = customersTable.concat('<tr><td>' + customer.CustomerID + '</td>' + '<td>' + customer.FirstName + " " + customer.LastName + '</td>' + '<td>' + customer.BirthDate + '</td>' + '<td>' + customer.PhoneNumber + '</td>' + '<td>' + customer.Gender + '</td>' + '<td>' + customer.State + '</td>' + '<td>' + customer.City + '</td></tr>');
    });
    customersTable = customersTable.concat('</tbody></table>');
    ChangeBodyContainerView(customersTable, '/Customers')
}

///////////////SalesScripts////////////////////////////

function SalesAdvancedSearchPage() {
    var SearchPage = '<h2>Advance Search Page</h2>';
    SearchPage = SearchPage.concat('<form class="form-inline md-form form-sm active-cyan-2"><input id="SaleID" class="form-control form-control-sm mr-3 w-75" type="text" placeholder="SaleID" aria-label="Search"></form>');
    SearchPage = SearchPage.concat('<form class="form-inline md-form form-sm active-cyan-2"><input id="CustomerID" class="form-control form-control-sm mr-3 w-75" type="text" placeholder="Email" aria-label="Search"></form>');
//    SearchPage = SearchPage.concat('<form class="form-inline md-form form-sm active-cyan-2"><div class="checkbox"><input id="Purchased" class="form-control form-control-sm mr-3 w-75" placeholder="Purchased" aria-label="Search"></form>');
    SearchPage = SearchPage.concat('<br/><button id="multiple-search-btn" type="submit" width="60" height="100" onclick="SalesAdvancedSearch()">GO!</button>');
    ChangeBodyContainerView(SearchPage, '/Sales');
}

function SalesAdvancedSearch() {
    var email = $("#CustomerID").val();
    var saleID = $("#SaleID").val();
    //var purchased = $("#Purchased").val();
    ToAjax('AdvancedSearch', 'Sales', SaleListToHtml, { "SaleID": saleID, "CustomerID": email /*, "Purchased": purchased*/ });
}

function SaleListToHtml(SalesList) {
    var salesTable = '<table class="table">';
    salesTable = salesTable.concat(headTable(["SaleID", "Email", "Purchased", "TotalPrice"]));
    $.each(SalesList, function (index, sale) {
        salesTable = salesTable.concat('<tr><td>' + sale.SaleID + '</td>' + '<td>' + sale.CustomerID + '</td>' + '<td>' + sale.Purchased + '<td>' + sale.TotalPrice + '</td>' + '</td></tr>');
    });
    salesTable = salesTable.concat('</tbody></table>');
    ChangeBodyContainerView(salesTable,'/Sales')
}

///////////////SupplierScripts////////////////////////////


function SuppliersAdvancedSearchPage() {
    var SearchPage = '<h2>Advance Search Page</h2>';
    SearchPage = SearchPage.concat('<form class="form-inline md-form form-sm active-cyan-2"><input id="Name" class="form-control form-control-sm mr-3 w-75" type="text" placeholder="Name" aria-label="Search"></form>');
    SearchPage = SearchPage.concat('<form class="form-inline md-form form-sm active-cyan-2"><input id="PhoneNumber" class="form-control form-control-sm mr-3 w-75" type="text" placeholder="Phone Number" aria-label="Search"></form>');
    SearchPage = SearchPage.concat('<form class="form-inline md-form form-sm active-cyan-2"><input id="Email" class="form-control form-control-sm mr-3 w-75" type="text" placeholder="Email" aria-label="Search"></form>');
    SearchPage = SearchPage.concat('<br/><button id="multiple-search-btn" type="submit" width="60" height="100" onclick="SalesAdvancedSearch()">GO!</button>');
    ChangeBodyContainerView(SearchPage, '/Customers');
}

//CustomerAdvancedSearch with 4 parameters
function SalesAdvancedSearch() {
    var name = $("#Name").val();
    var phone = $("#PhoneNumber").val();
    var email = $("#Email").val();
    ToAjax('AdvancedSearch', 'Suppliers', SupplierListToHtml, { "Name": name, "MailAddress": email, "PhoneNumber": phone });
}

//Builds customer table after search
function SupplierListToHtml(SuppliersList) {
    var customersTable = '<table class="table">';
    customersTable = customersTable.concat(headTable(["SupplierID", "Name", "PhoneNumber", "MailAddress"]));
    $.each(SuppliersList, function (index, supplier) {
        customersTable = customersTable.concat('<tr><td>' + supplier.SupplierID + '</td>' + '<td>' + supplier.Name + '</td>' + '<td>' + supplier.PhoneNumber + '</td>' + '<td>' + supplier.MailAddress + '</td></tr>');
    });
    customersTable = customersTable.concat('</tbody></table>');
    ChangeBodyContainerView(customersTable, '/Customers')
}

