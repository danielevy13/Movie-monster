function MovieListToHtml(movies){
	 var moviesTable = '<div id="bodyContainer" class="container" style="background-color: #ffffff;"><table class="table"><thead><tr><th>Title</th><th>Genere</th><th>UnitsInStock</th><th>ReleaseDate</th><th>Actors</th><th>MinAge</th><th>Language</th><th>UnitPrice</th><th></th></thead><tbody>';
        $.each(movies, function (index, movie) {
            moviesTable = moviesTable.concat('<tr><td>' + movie.Title + '</td>' + '<td>' + movie.Genere + '</td>' + '<td>' + movie.UnitsInStock + '</td>' + '<td>' + movie.ReleaseDate + '</td>' + '<td>' + movie.Actors + '</td>' + '<td>' + movie.MinAge + '</td>' + '<td>' + movie.Language + '</td>' + '<td>' + movie.UnitPrice.Wholesale + '</td></tr>');
        })
        moviesTable = moviesTable.concat('</tbody></table></div>');
        return moviesTable;
}