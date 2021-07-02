import React, { useMemo } from "react";
import { useSortBy, useTable } from "react-table";
import { COLUMNS } from "./ReportColumn.js";
import { useStore } from "../../api/store";
import { observer } from "mobx-react-lite";

export default observer(function ReportTable() {
  const { reportStore } = useStore();
  const { listReport } = reportStore;

  const reportColumn = useMemo(() => COLUMNS, [COLUMNS]);
  const reportData = useMemo(() => listReport, [listReport]);

  const tableInstance = useTable(
    {
      columns: reportColumn,
      data: reportData,
      initialState: {
        sortBy: [{ id: "category", desc: false }],
      },
    },
    useSortBy
  );

  const { getTableProps, getTableBodyProps, headerGroups, rows, prepareRow } =
    tableInstance;

  return (
    <div className="mt-4">
      <table {...getTableProps()}>
        <thead>
          {headerGroups.map((headerGroup) => (
            <tr {...headerGroup.getHeaderGroupProps()}>
              {headerGroup.headers.map((column) => (
                <th
                  className="header-data-column"
                  {...column.getHeaderProps(column.getSortByToggleProps())}
                >
                  {column.render("header")}
                  <span>
                    {column.isSorted ? (
                      column.isSortedDesc ? (
                        <span>&#x25BC;</span>
                      ) : (
                        <span>&#x25B2;</span>
                      )
                    ) : (
                      ""
                    )}
                  </span>
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
                    <td
                      className="data-column"
                      {...cell.getCellProps({
                        style: { width: cell.column.width },
                      })}
                    >
                      {cell.render("Cell")}
                    </td>
                  );
                })}
              </tr>
            );
          })}
        </tbody>
      </table>
    </div>
  );
});
