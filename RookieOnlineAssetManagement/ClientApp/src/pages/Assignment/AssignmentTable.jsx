import { useMemo } from "react";
import { useSortBy, useTable } from "react-table";
import { COLUMNS } from "./AssignmentColumn.js";
import { useStore } from "../../api/store";
import { observer } from "mobx-react-lite";
import ModalAssignmentDetail from "../../components/modal/ModalAssignmentDetail.jsx";
import LoadingComponent from "../../components/LoadingComponent.jsx";
import { history } from "../../index.jsx";
import ConfirmDeleteAssignment from "../../components/modal/ConfirmDeleteAssignment.jsx";
import ConfirmReqReturnAssignment from "../../components/modal/ConfirmReqReturnAssignment";

export default observer(function AssignmentTable() {
  const { modalStore, assignmentStore } = useStore();
  const { loadingInitial, assignmentBySort, loadAssignment, filter } =
    assignmentStore;

  const assignColumn = useMemo(() => COLUMNS, [COLUMNS]);
  const assignData = useMemo(() => assignmentBySort, [assignmentBySort]);

  const tableInstance = useTable(
    {
      columns: assignColumn,
      data: assignData,
      initialState: {
        hiddenColumns: ["assignmentId", "specification", "note"],
      },
    },
    useSortBy
  );

  const { getTableProps, getTableBodyProps, headerGroups, rows, prepareRow } =
    tableInstance;

  if (loadingInitial) {
    return <LoadingComponent content="Loading assignment table...." />;
  }

  if (assignmentBySort.length === 0 && filter.get("QueryString") !== "") {
    return (
      <div className="mt-5">
        <h3>Your search does not match any results!</h3>
      </div>
    );
  }

  //Seperate Column to set ClassName
  var setClassName_data = (header, state, id) => {
    if (header !== "") {
      return "data-column";
    } else if (state === "Accepted" || state === "Declined") {
      if (id === "editAssign") {
        return "disable-icon-edit disableButton";
      } else if (id === "removeAssign") {
        return "disable-icon-remove disableButton";
      } else if (state === "Declined") {
        return "disableButton";
      } else {
        return "return-icon";
      }
    } else {
      if (id === "returnAssign") {
        return "disableButton";
      }
    }
  };
  var setClassName_header = (header) => {
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
                      onClick: async () => {
                        if (cell.column.header !== "") {
                          modalStore.openDetaiModal(
                            <ModalAssignmentDetail />,
                            await loadAssignment(row.values.assignmentId)
                          );
                          console.log(row.values);
                        } else if (cell.column.id === "editAssign") {
                          history.push(
                            `/assignments/edit/${cell.row.values.assignmentId}`
                          );
                        } else if (cell.column.id === "removeAssign") {
                          modalStore.openModal(
                            <ConfirmDeleteAssignment
                              assignment={cell.row.values}
                            />,
                            "md"
                          );
                        } else {
                          modalStore.openModal(
                            <ConfirmReqReturnAssignment
                              id={cell.row.values.assignmentId}
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
  );
});
