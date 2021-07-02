import { useMemo } from "react";
import { useSortBy, useTable } from "react-table";
import { COLUMNS } from "./RequestColumn.js";
import { useStore } from "../../api/store";
import { observer } from "mobx-react-lite";
import CancelRequest from "../../components/modal/CancelRequest.jsx";
import CompleteRequest from "../../components/modal/CompleteRequest.jsx";

export const RequestTable = observer(() => {
  const { returnStore, modalStore } = useStore();
  const { listReturn, filter } = returnStore;
  const requestColumn = useMemo(() => COLUMNS, [COLUMNS]);
  const requestData = useMemo(() => listReturn, [listReturn]);
  const tableInstance = useTable(
    {
      columns: requestColumn,
      data: requestData,
      initialState: {
        sortBy: [{ id: "assetCode", desc: false }],
        hiddenColumns: ["returnId"],
      },
    },
    useSortBy
  );

  const { getTableProps, getTableBodyProps, headerGroups, rows, prepareRow } =
    tableInstance;


  if (listReturn.length === 0 && filter.get("QueryString") !== "") {
    return (
      <div className="mt-5">
        <h3>Your search does not match any results!</h3>
      </div>
    );
  }

  //Seperate Column to set ClassName
  const setClassName_data = (header, state, id) => {
    if (header !== "") {
      return "data-column";
    } else if (state === "Completed") {
      if (id === "checkReturn") {
        return "disable-icon-check";
      } else {
        return "disable-icon-x";
      }
    }
  };
  const setClassName_header = (header) => {
    if (header !== "") {
      return "header-data-column";
    } else {
      return "header-funtion-column";
    }
  };

  return (
    <table {...getTableProps()}>
      <thead>
        {headerGroups.map((headerGroup) => (
          <tr {...headerGroup.getHeaderGroupProps()}>
            {headerGroup.headers.map((column) => (
              <th
                className={setClassName_header(column.header)}
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
                    className={setClassName_data(
                      cell.column.header,
                      cell.row.values.state,
                      cell.column.id
                    )}
                    {...cell.getCellProps({
                      onClick: () => {
                        if (cell.column.header !== "") {
                        } else if (cell.column.id === "checkReturn") {
                          modalStore.openModal(
                            <CompleteRequest id={cell.row.values.returnId} />,
                            "md"
                          );
                        } else {
                          modalStore.openModal(
                            <CancelRequest returnAssign={cell.row.values} />,
                            "md"
                          );
                        }
                      },
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
  );
});
