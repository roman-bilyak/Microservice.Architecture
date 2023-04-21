import React, { useState } from "react";
import DataTable from "react-data-table-component";
import { MoviesAPIService, MovieDto } from "../api-services/api-services";

export const MoviesComponent = () => {
  const [items, setItems] = useState<MovieDto[]>([]);
  const [loading, setLoading] = useState(false);
  const [totalCount, setTotalCount] = useState(0);
  const [currentPage, setCurrentPage] = useState(1);
  const [perPage, setPerPage] = useState(5);

  const columns = [
    {
      name: 'Title',
      selector: (row: MovieDto) => row.title || ''
    }];

  const fetchData = async (page: number, size: number | undefined = perPage) => {
    setLoading(true);

    let moviesAPIService = new MoviesAPIService('https://localhost:7100'); //TODO: move API base url into config file
    var response = await moviesAPIService.getMovieList(page - 1, size);

    setItems(response.items || []);
    setTotalCount(response.totalCount);
    setLoading(false);
  };

  const handlePageChange = async (page: number) => {
    await fetchData(page);
    setCurrentPage(page);
  };

  const handlePerRowsChange = async (newPerPage: number, page: number) => {
    await fetchData(page, newPerPage);
    setPerPage(newPerPage);
  };

  return (
    <DataTable
      columns={columns}
      data={items}

      progressPending={loading}

      pagination
      paginationServer

      paginationTotalRows={totalCount}
      paginationDefaultPage={currentPage}
      paginationPerPage={perPage}
      paginationRowsPerPageOptions={[5, 10, 20]}

      onChangeRowsPerPage={handlePerRowsChange}
      onChangePage={handlePageChange}
    />
  );
}