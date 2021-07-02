import { FaCheck } from "react-icons/fa/index.js";
import { IoCloseSharp } from "react-icons/io5/index.js";

export const COLUMNS = [
  {
    header: "No.",
    accessor: "index",
    Cell: (row) => {
      return <div>{+row.row.id + 1}</div>;
    },
    disableSortBy: true,
    disableFilters: true,
    width: 40,
  },
  {
    header: "Id",
    accessor: "returnId",
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
    header: "Requested by",
    accessor: "requestedBy",
  },
  {
    header: "Assigned Date",
    accessor: "assignedDate",
  },
  {
    header: "Accepted By",
    accessor: "acceptedBy",
  },
  {
    header: "Returned Date",
    accessor: "returnedDate",
  },
  {
    header: "State",
    accessor: "state",
  },
  {
    header: "",
    id: "checkReturn",
    Cell: () => {
      return <FaCheck color="#D30F18" size={20}></FaCheck>;
    },
    width: 40,
  },
  {
    header: "",
    id: "removeReturn",
    Cell: () => {
      return <IoCloseSharp color="#282525" size={22}></IoCloseSharp>;
    },
    width: 40,
  },
];
