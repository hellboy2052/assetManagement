import { Grid, Input, Button, Checkbox } from "semantic-ui-react";
import {
  Dropdown,
  DropdownToggle,
  DropdownMenu,
  DropdownItem,
} from "reactstrap";
import React, { useState } from "react";
import { useStore } from "../../api/store";
import { observer } from "mobx-react-lite";
import { NavLink } from "react-router-dom";
import { typeOptions } from "../../components/option/TypeOption";

export default observer(function UserHeader() {
  const { userStore } = useStore();
  const [filter, setFilter] = useState(
    userStore.filter.get("Type").length === typeOptions.length
      ? ["All"]
      : userStore.filter.get("Type")
  );

  const handleFilter = (e, { value }) => {
    if (value === "All") {
      setFilter([value]);
      userStore.setFilter(typeOptions.map((t) => t.text));
    } else if (filter.includes(value) && filter.length === 1) {
      setFilter(["All"]);
      userStore.setFilter(typeOptions.map((t) => t.text));
    } else {
      if (!filter.includes(value)) {
        setFilter([...filter.filter((f) => f !== "All"), value]);
        userStore.setFilter([...filter.filter((f) => f !== "All"), value]);
      } else {
        setFilter([...filter.filter((f) => f !== value)]);
        userStore.setFilter([...filter.filter((f) => f !== value)]);
      }
    }
  };
  const [dropdownOpen, setDropdownOpen] = useState(false);

  const toggle = () => setDropdownOpen((prevState) => !prevState);
  const handleSearch = (e) =>
    userStore.setSearchQuery(e.currentTarget.previousSibling.value);
  return (
    <>
      <Grid columns={3}>
        <Grid.Row centered>
          <Grid.Column>
            <div className="input-group">
              <div
                style={{
                  display: "inherit",
                  border: "2px solid rgb(214 214 214)",
                  borderRadius: "5px",
                }}
              >
                <Dropdown isOpen={dropdownOpen} toggle={toggle}>
                  <DropdownToggle
                    color="white"
                    style={{ width: "110px", textAlign: "left" }}
                  >
                    Type
                  </DropdownToggle>
                  <Button
                    style={{ margin: "0", height: "100%", color: "black" }}
                    size="mini"
                    icon="filter"
                    onClick={toggle}
                  />
                  <DropdownMenu>
                    <DropdownItem>
                      <Checkbox
                        label="All"
                        value="All"
                        checked={filter.includes("All")}
                        onClick={handleFilter}
                      />
                    </DropdownItem>
                    <DropdownItem>
                      <Checkbox
                        label="Admin"
                        value="Admin"
                        checked={filter.includes("Admin")}
                        onClick={handleFilter}
                      />
                    </DropdownItem>
                    <DropdownItem>
                      <Checkbox
                        label="Staff"
                        value="Staff"
                        checked={filter.includes("Staff")}
                        onClick={handleFilter}
                      />
                    </DropdownItem>
                  </DropdownMenu>
                </Dropdown>
              </div>
            </div>
          </Grid.Column>
          <Grid.Column>
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
          </Grid.Column>
          <Grid.Column>
            <div
              className="input-group"
              style={{
                alignContent: "center",
                justifyContent: "center",
                height: "100%",
              }}
            >
              <Button
                content="Create new User"
                color="red"
                size="mini"
                as={NavLink}
                to="/users/create"
              />
            </div>
          </Grid.Column>
        </Grid.Row>
      </Grid>
    </>
  );
});
