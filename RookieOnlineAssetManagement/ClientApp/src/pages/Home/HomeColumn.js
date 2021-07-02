import { FaCheck } from "react-icons/fa/index.js";
import { IoReloadCircle, IoCloseSharp } from "react-icons/io5/index.js";

export const COLUMNS = [
  {
    header: "AssignmentID",
    accessor: "assignmentId",
  },
  {
    header: "Asset Code",
    accessor: "assetCode",
    width: 120,
  },
  {
    header: "Asset Name",
    accessor: "assetName",
    width: 250,
  },
  {
    header: "Category",
    accessor: "category",
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
    width: 200,
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
    id: "checkedAssign",
    Cell: () => {
      return <FaCheck color="#D30F18" size={20}></FaCheck>;
    },
    width: 40,
  },
  {
    header: "",
    id: "declineAssign",
    Cell: () => {
      return <IoCloseSharp color="#282525" size={22}></IoCloseSharp>;
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
