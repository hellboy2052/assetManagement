import { useMemo } from "react";
import { useSortBy, useTable } from "react-table";
import { COLUMNS } from "./HomeColumn.js";
import { useStore } from "../../api/store";
import ModalAssignmentDetail from "../../components/modal/ModalAssignmentDetail.jsx";
import ConfirmHomeAsset from "../../components/modal/ConfirmHomeAsset";
import {
  ACCEPT_ASSIGNMENT_STATE,
  DECLINE_ASSIGNMENT_STATE,
  RETURN_ASSIGNMENT_STATE,
} from "../../constant/assignmentState";
import LoadingComponent from "../../components/LoadingComponent.jsx";
import { observer } from "mobx-react-lite";

export const HomeTable = observer(() => {
  const { modalStore, identityStore } = useStore();
  const { loadingInitial, assignList, loadAssignment } = identityStore;

  const homeColumn = useMemo(() => COLUMNS, [COLUMNS]);
  const homeData = useMemo(() => assignList, [assignList]);

  const tableInstance = useTable(
    {
      columns: homeColumn,
      data: homeData,
      initialState: {
        hiddenColumns: [
          "assignmentId",
          "specification",
          "note",
          "assignedTo",
          "assignedBy",
        ],
      },
    },
    useSortBy
  );

  const { getTableProps, getTableBodyProps, headerGroups, rows, prepareRow } =
    tableInstance;

  if (loadingInitial) {
    return <LoadingComponent content="Loading Assignment...." />;
  }
  //Seperate Column to set ClassName
  var setClassName_data = (header, state, id) => {
    if (header !== "") {
      return "data-column";
    } else if (state === "Accepted") {
      if (id === "checkedAssign") {
        return "disable-icon-check disableButton";
      } else if (id === "declineAssign") {
        return "disable-icon-x disableButton";
      } else {
        return "return-icon";
      }
    } else if (id === "returnAssign") {
      return "disable-return-icon disableButton";
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
    <>
      {!loadingInitial && (
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
                            } else if (cell.column.id === "checkedAssign") {
                              modalStore.openModal(
                                <ConfirmHomeAsset
                                  assignmentState={ACCEPT_ASSIGNMENT_STATE}
                                  assignmentId={cell.row.values.assignmentId}
                                />
                              );
                            } else if (cell.column.id === "declineAssign") {
                              modalStore.openModal(
                                <ConfirmHomeAsset
                                  assignmentState={DECLINE_ASSIGNMENT_STATE}
                                  assignmentId={cell.row.values.assignmentId}
                                />
                              );
                            } else {
                              modalStore.openModal(
                                <ConfirmHomeAsset
                                  assignmentState={RETURN_ASSIGNMENT_STATE}
                                  assignmentId={cell.row.values.assignmentId}
                                />
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
      )}
    </>
  );
});
