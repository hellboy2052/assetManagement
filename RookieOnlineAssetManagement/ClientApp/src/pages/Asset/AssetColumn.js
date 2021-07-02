import { TiDeleteOutline } from "react-icons/ti/index.js";
import { GoPencil } from "react-icons/go/index.js";

export const COLUMNS = [
  {
    header: "Asset Code",
    accessor: "assetCode",
  },
  {
    header: "Asset Name",
    accessor: "assetName",
    width: "35%",
  },
  {
    header: "Category",
    accessor: "category",
    width: "20%",
  },
  {
    header: "Installed Date",
    accessor: "installDate",
  },
  {
    header: "State",
    accessor: "state",
  },
  {
    header: "Location",
    accessor: "location",
  },
  {
    header: "Specification",
    accessor: "specification",
  },
  {
    header: "Assign",
    accessor: "isAssigned",
  },
  {
    header: "",
    id: "editAsset",
    Cell: () => {
      return <GoPencil color="#656565" size={22}></GoPencil>;
    },
    width: 45,
  },
  {
    header: "",
    id: "removeAsset",
    Cell: () => {
      return <TiDeleteOutline color="#BF103A" size={22}></TiDeleteOutline>;
    },
    width: 45,
  },
];
