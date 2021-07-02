import React, { useMemo } from "react";
import { COLUMNS } from "./UserColumn";
import "./table.css";
import { useTable, useSortBy } from "react-table";
import { observer } from "mobx-react-lite";
import { useStore } from "../../api/store";
import LoadingComponent from "../../components/LoadingComponent";
import ModalUserDetail from "../../components/modal/ModalUserDetail";
import ConfirmDisableUser from "../../components/modal/ConfirmDisableUser";
import { history } from "../..";

export default observer(function UserTable() {
  const { userStore, modalStore } = useStore();
  const { userByNameSort, loadingInitial, filter } = userStore;

  const userColumn = useMemo(() => COLUMNS, [COLUMNS]);
  const userData = useMemo(() => userByNameSort, [userByNameSort]);

  const tableInstance = useTable(
    {
      columns: userColumn,
      data: userData,
      initialState: {
        hiddenColumns: [
          "dateOfBirth",
          "gender",
          "location",
          "id",
          "isAssigned",
        ],
      },
    },
    useSortBy
  );

  const { getTableProps, getTableBodyProps, headerGroups, rows, prepareRow } =
    tableInstance;

  if (loadingInitial) {
    return <LoadingComponent content="Loading user table...." />;
  }

  if (userByNameSort.length === 0 && filter.get("QueryString") !== "") {
    return (
      <div className="mt-5">
        <h3>Your search does not match any results!</h3>
      </div>
    );
  }

  //Seperate Column to set ClassName
  var IsDataColumn = (header) => {
    if (header !== "") {
      return "data-column";
    } else {
      return "funtion-column";
    }
  };
  var IsDataHeader = (header) => {
    if (header !== "") {
      return "header-data-column";
    } else {
      return "header-funtion-column";
    }
  };

  return (
    <div className="mt-5">
      <table className="primary-table" {...getTableProps()}>
        <thead>
          {headerGroups.map((headerGroup) => (
            <tr {...headerGroup.getHeaderGroupProps()}>
              {headerGroup.headers.map((column) => (
                <th
                  className={IsDataHeader(column.header)}
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
                      className={IsDataColumn(cell.column.header)}
                      {...cell.getCellProps({
                        onClick: () => {
                          if (cell.column.header !== "") {
                            modalStore.openDetaiModal(
                              <ModalUserDetail />,
                              row.values
                            );
                          } else if (cell.column.id === "editUser") {
                            history.push(`/users/edit/${cell.row.values.id}`);
                          } else {
                            modalStore.openModal(
                              <ConfirmDisableUser
                                id={cell.row.values.id}
                                assign={cell.row.values.isAssigned}
                              />,
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
    </div>
  );
});
