import React, { useMemo, useState, useEffect } from "react";
import { Input } from "semantic-ui-react";
import { useSortBy, useTable, useRowSelect } from "react-table";
import { COLUMNS } from "./UserColumn";
import { useStore } from "../../../api/store";
import { observer } from "mobx-react-lite";
import { Button, Row, Col } from "reactstrap";
import "./SelectTable.css";

export default observer(function SelectUserTable(prop) {
  const { modalStore, userStore } = useStore();
  const { setTableSearchQuery, loadTableUsers, tableUser } = userStore;

  const assetColumn = useMemo(() => COLUMNS, [COLUMNS]);
  const assetData = useMemo(() => tableUser, [tableUser]);

  useEffect(() => {
    loadTableUsers();
  }, [loadTableUsers]);

  const [selectedRowId, setSelectedRowId] = useState(null);
  const selectedRowIds = [];
  if (selectedRowId) {
    selectedRowIds.push(selectedRowId);
  }

  const IndeterminateCheckbox = React.forwardRef(
    ({ indeterminate, ...rest }, ref) => {
      const defaultRef = React.useRef();
      const resolvedRef = ref || defaultRef;

      React.useEffect(() => {
        resolvedRef.current.indeterminate = indeterminate;
      }, [resolvedRef, indeterminate]);

      return (
        <>
          <input type="radio" name="radio" ref={resolvedRef} {...rest} />
        </>
      );
    }
  );

  const tableInstance = useTable(
    {
      columns: assetColumn,
      data: assetData,
      autoResetSelectedRows: true,
      initialState: {
        selectedRowIds,
        hiddenColumns: [
          "dateOfBirth",
          "gender",
          "location",
          "id",
          "isAssigned",
          "userName",
          "joinedDate",
        ],
        sortBy: [{ id: "staffCode", desc: false }],
      },
    },
    useSortBy,
    useRowSelect,
    (hooks) => {
      hooks.visibleColumns.push((columns) => [
        // Let's make a column for selection
        {
          id: "selection",
          // The cell can use the individual row's getToggleRowSelectedProps method
          // to the render a checkbox
          Cell: ({ row }) => (
            <div>
              <IndeterminateCheckbox
                onClick={() => setSelectedRowId(row.accessCode)}
                {...row.getToggleRowSelectedProps()}
              />
            </div>
          ),
        },
        ...columns,
      ]);
    }
  );

  const {
    getTableProps,
    getTableBodyProps,
    headerGroups,
    rows,
    prepareRow,
    selectedFlatRows,
  } = tableInstance;
  var setClassName_data = (header, state, id) => {
    if (header !== "") {
      return "data-column";
    } else if (state === "Assigned") {
      if (id === "editAsset") return "disable-icon-edit";
      return "disable-icon-remove";
    }
  };
  var setClassName_header = (header) => {
    if (header !== "") {
      return "header-data-column";
    } else {
      return "header-funtion-column";
    }
  };

  const handleSearch = (e) => {
    setTableSearchQuery(e.currentTarget.previousSibling.value);
  };

  return (
    <>
      <Row className="TableHeader mt-3">
        <Col md={1}></Col>
        <Col md={4}>
          <h4 className="SelectHeader">Select User</h4>
        </Col>
        <Col md={1}></Col>
        <Col md={6}>
          <div className="input-group">
            <div>
              <Input
                action={{
                  icon: "search",
                  onClick: (e) => handleSearch(e),
                  name: "search",
                }}
                defaultValue={userStore.filter.get("QueryString")}
                placeholder="Search..."
              />
            </div>
          </div>
          <div
            className="input-group"
            style={{
              alignContent: "center",
              justifyContent: "center",
              height: "100%",
            }}
          ></div>
        </Col>
      </Row>
      <div className="mt-3">
        <table {...getTableProps()}>
          <thead>
            {headerGroups.map((headerGroup) => (
              <tr {...headerGroup.getHeaderGroupProps()}>
                {headerGroup.headers.map((column) => (
                  <th
                    className={setClassName_header(column.Header)}
                    {...column.getHeaderProps(column.getSortByToggleProps())}
                  >
                    {column.render("Header")}
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
                          cell.column.Header,
                          cell.row.values.state,
                          cell.column.id
                        )}
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
        <div className="mt-3">
          <Button
            className="SaveBtn"
            onClick={() => {
              prop.handleSelectUser(
                "User",
                {
                  id: selectedFlatRows[0].values.id,
                  Fullname: selectedFlatRows[0].values.fullName,
                },
                false
              );
              prop.handleSetTouch("Note", true);
              modalStore.closeModal();
            }}
            disabled={!selectedFlatRows[0]}
            color="danger"
          >
            Save
          </Button>
          <Button onClick={modalStore.closeModal}>Cancel</Button>
        </div>
      </div>
    </>
  );
});
