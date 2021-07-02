import { TiDeleteOutline } from "react-icons/ti/index.js";
import { GoPencil } from "react-icons/go/index.js";

export const COLUMNS = [
  {
    header: "IdUser",
    accessor: "id",
  },
  {
    header: "Staff Code",
    accessor: "staffCode",
    width: 100,
  },
  {
    header: "Full Name",
    accessor: "fullName",
    width: 200,
  },
  {
    header: "User Name",
    accessor: "userName",
    width: 130,
  },
  {
    header: "Date of Birth",
    accessor: "dateOfBirth",
  },
  {
    header: "Gender",
    id: "gender",
    accessor: (g) => (g.gender ? "Male" : "Female"),
  },
  {
    header: "Joined Date",
    accessor: "joinedDate",
    width: 170,
  },
  {
    header: "Type",
    accessor: "type",
    width: 130,
  },
  {
    header: "Location",
    accessor: "location",
  },
  {
    header: "",
    id: "editUser",
    Cell: () => {
      return <GoPencil color="#656565" size={22}></GoPencil>;
    },
    width: 45,
  },
  {
    header: "Assign",
    accessor: "isAssigned",
  },
  {
    header: "",
    id: "removeUser",
    Cell: () => {
      return <TiDeleteOutline color="#BF103A" size={22}></TiDeleteOutline>;
    },
    width: 45,
  },
];
