import { TiDeleteOutline } from "react-icons/ti/index.js";
import { GoPencil } from "react-icons/go/index.js";
import { IoReloadCircle } from "react-icons/io5/index.js";

export const COLUMNS = [
  {
    header: "No.",
    accessor: "index",
    Cell: (row) => {
      return <div>{+row.row.id + 1}</div>;
    },
    disableSortBy: true,
    disableFilters: true,
    width: 50,
  },
  {
    header: "AssignmentID",
    accessor: "assignmentId",
  },
  {
    header: "Asset Code",
    accessor: "assetCode",
  },
  {
    header: "Asset Name",
    accessor: "assetName",
    width: 200,
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
    header: "Assigned Date",
    accessor: "assignedDate",
  },
  {
    header: "State",
    accessor: "state",
  },
  {
    header: "Specification",
    accessor: "specification",
  },
  {
    header: "Note",
    accessor: "note",
  },
  {
    header: "",
    id: "editAssign",
    Cell: () => {
      return <GoPencil color="#656565" size={22}></GoPencil>;
    },
    width: 40,
  },
  {
    header: "",
    id: "removeAssign",
    Cell: () => {
      return <TiDeleteOutline color="#BF103A" size={22}></TiDeleteOutline>;
    },
    width: 40,
  },
  {
    header: "",
    id: "returnAssign",
    Cell: () => {
      return <IoReloadCircle color="#656565" size={22}></IoReloadCircle>;
    },
    width: 40,
  },
];
