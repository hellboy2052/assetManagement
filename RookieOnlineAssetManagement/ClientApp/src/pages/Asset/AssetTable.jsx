import { useMemo } from "react";
import { useSortBy, useTable } from "react-table";
import { COLUMNS } from "./AssetColumn.js";
import ModalAssetDetail from "../../components/modal/ModalAssetDetail";
import LoadingComponent from "../../components/LoadingComponent";
import { useStore } from "../../api/store";
import { observer } from "mobx-react-lite";
import { history } from "../../index.jsx";
import ConfirmDeleteAsset from "../../components/modal/ConfirmDeleteAsset";

export default observer(function AssetTable() {
  const { modalStore, assetStore } = useStore();
  const { assetByNameSort, loadingInitial, filter, loadAssetDetail } =
    assetStore;

  const assetColumn = useMemo(() => COLUMNS, [COLUMNS]);
  const assetData = useMemo(() => assetByNameSort, [assetByNameSort]);

  const tableInstance = useTable(
    {
      columns: assetColumn,
      data: assetData,
      initialState: {
        hiddenColumns: [
          "installDate",
          "location",
          "specification",
          "isAssigned",
        ],
      },
    },
    useSortBy
  );

  const { getTableProps, getTableBodyProps, headerGroups, rows, prepareRow } =
    tableInstance;

  if (loadingInitial) {
    return <LoadingComponent content="Loading asset table...." />;
  }
  if (assetByNameSort.length === 0 && filter.get("QueryString") !== "") {
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
    } else if (state === "Assigned") {
      if (id === "editAsset") return "disable-icon-edit disableButton";
      return "disable-icon-remove disableButton";
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
    <div className="mt-5">
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
                          if (
                            cell.column.id === "removeAsset" &&
                            cell.row.values.state !== "Assigned"
                          ) {
                            modalStore.openModal(
                              <ConfirmDeleteAsset
                                id={cell.row.values.assetCode}
                                assign={cell.row.values.isAssigned}
                              />,
                              "md"
                            );
                          } else if (
                            cell.column.id === "editAsset" &&
                            cell.row.values.state !== "Assigned"
                          ) {
                            history.push(
                              `/assets/edit/${cell.row.values.assetCode}`
                            );
                          } else {
                            modalStore.openDetaiModal(
                              <ModalAssetDetail />,
                              await loadAssetDetail(row.values.assetCode)
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
