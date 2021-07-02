import React, { useMemo } from "react";
import { useTable } from "react-table";

const COLUMNS = [
  {
    header: "Date",
    accessor: "assignedDate",
  },
  {
    header: "Assigned To",
    accessor: "assignedTo",
  },
  {
    header: "Assigned By",
    accessor: "assignedBy",
  },
  {
    header: "Returned Date",
    accessor: "returnedDate",
  },
];

export const HistoryTable = ({ histories }) => {
  const historyColumn = useMemo(() => COLUMNS, [COLUMNS]);
  const historyData = useMemo(() => histories, [histories]);

  const tableInstance = useTable({
    columns: historyColumn,
    data: historyData,
  });

  const { getTableProps, getTableBodyProps, headerGroups, rows, prepareRow } =
    tableInstance;

  return (
    <table {...getTableProps()}>
      <thead>
        {headerGroups.map((headerGroup) => (
          <tr {...headerGroup.getHeaderGroupProps()}>
            {headerGroup.headers.map((column) => (
              <th className="header-data-column" {...column.getHeaderProps()}>
                {column.render("header")}
              </th>
            ))}
          </tr>
        ))}
      </thead>
      <tbody {...getTableBodyProps()}>
        {rows.map((row) => {
          prepareRow(row);
          return (
            <tr {...row.getRowProps()}>
              {row.cells.map((cell) => {
                return (
                  <td className="data-column" {...cell.getCellProps({})}>
                    {cell.render("Cell")}
                  </td>
                );
              })}
            </tr>
          );
        })}
      </tbody>
    </table>
  );
};
