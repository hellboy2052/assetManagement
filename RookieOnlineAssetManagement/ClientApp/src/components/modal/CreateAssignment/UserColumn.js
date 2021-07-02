

export const COLUMNS = [
  {
    Header: "IdUser",
    accessor: "id"
  },
  {
    Header: "Staff Code",
    accessor: "staffCode",
  },
  {
    Header: "Full Name",
    accessor: "fullName",
  },
  {
    Header: "User Name",
    accessor: "userName",
  },
  {
    Header: "Date of Birth",
    accessor: "dateOfBirth",
  },
  {
    Header: "Gender",
    id: "gender",
    accessor: (g) => (g.gender ? "Male" : "Female"),
  },
  {
    Header: "Joined Date",
    accessor: "joinedDate",
  },
  {
    Header: "Type",
    accessor: "type",
  },
  {
    Header: "Location",
    accessor: "location",
  },
  {
    Header: "Assign",
    accessor: "isAssigned"
  }
];
