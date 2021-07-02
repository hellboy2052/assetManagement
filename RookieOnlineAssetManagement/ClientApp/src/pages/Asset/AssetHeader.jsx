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
import { stateOptions } from "../../components/option/StateOption";

export default observer(function AssetHeader() {
  const { categoryStore, assetStore } = useStore();

  const [dropdownOpen, setDropdownOpen] = useState(false);
  const [stateDropdown, setstateDropdown] = useState(false);
  const { categoryOption } = categoryStore;
  const { setCategoryFilter, setStateFilter, setSearchQuery } = assetStore;

  const [state, setState] = useState(
    assetStore.filter.get("States").length === stateOptions.length
      ? [0]
      : assetStore.filter.get("States")
  );
  const [catefilter, setFilter] = useState(
    assetStore.filter.get("Category").length === categoryOption.length
      ? ["All"]
      : assetStore.filter.get("Category")
  );

  const handleFilter = (e, { value }) => {
    if (value === "All") {
      setFilter([value]);
      setCategoryFilter([...categoryOption.map((c) => c.text)]);
    } else if (catefilter.includes(value) && catefilter.length === 1) {
      setFilter(["All"]);
      setCategoryFilter([...categoryOption.map((c) => c.text)]);
    } else {
      if (!catefilter.includes(value)) {
        setFilter([...catefilter.filter((f) => f !== "All"), value]);
        setCategoryFilter([...catefilter.filter((f) => f !== "All"), value]);
      } else {
        setFilter([...catefilter.filter((s) => s !== value)]);
        setCategoryFilter([...catefilter.filter((s) => s !== value)]);
      }
    }
  };
  const handleStateFilter = (e, { value }) => {
    if (value === 0) {
      setState([value]);
      setStateFilter([...stateOptions.map((c) => c.value)]);
    } else if (state.includes(value) && state.length === 1) {
      setState([0]);
      setStateFilter([...stateOptions.map((c) => c.value)]);
    } else {
      if (!state.includes(value)) {
        setState([...state.filter((s) => s !== 0), value]);
        setStateFilter([...state.filter((s) => s !== 0), value]);
      } else {
        setState([...state.filter((s) => s !== value)]);
        setStateFilter([...state.filter((s) => s !== value)]);
      }
    }
  };
  const toggle = () => setDropdownOpen((prevState) => !prevState);
  const toggleState = () => setstateDropdown((prevState) => !prevState);
  const handleSearch = (e) =>
    setSearchQuery(e.currentTarget.previousSibling.value);
  return (
    <>
      <Grid columns={4}>
        <Grid.Row centered>
          {/* Category filter */}
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
                    Category
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
                        value={"All"}
                        checked={catefilter.includes("All")}
                        onClick={handleFilter}
                      />
                    </DropdownItem>
                    {categoryOption &&
                      categoryOption.map((category) => (
                        <DropdownItem key={category.value}>
                          <Checkbox
                            label={category.text}
                            value={category.text}
                            checked={catefilter.includes(category.text)}
                            onClick={handleFilter}
                          />
                        </DropdownItem>
                      ))}
                  </DropdownMenu>
                </Dropdown>
              </div>
            </div>
          </Grid.Column>
          {/* State Filter */}
          <Grid.Column>
            <div className="input-group">
              <div
                style={{
                  display: "inherit",
                  border: "2px solid rgb(214 214 214)",
                  borderRadius: "5px",
                }}
              >
                <Dropdown isOpen={stateDropdown} toggle={toggleState}>
                  <DropdownToggle
                    color="white"
                    style={{ width: "110px", textAlign: "left" }}
                  >
                    State
                  </DropdownToggle>
                  <Button
                    style={{ margin: "0", height: "100%", color: "black" }}
                    size="mini"
                    icon="filter"
                    onClick={toggleState}
                  />
                  <DropdownMenu>
                    <DropdownItem>
                      <Checkbox
                        label="All"
                        value={0}
                        checked={state.includes(0)}
                        onClick={handleStateFilter}
                      />
                    </DropdownItem>
                    {stateOptions.map((s) => (
                      <DropdownItem key={s.value}>
                        <Checkbox
                          label={s.text}
                          value={s.value}
                          checked={state.includes(s.value)}
                          onClick={handleStateFilter}
                        />
                      </DropdownItem>
                    ))}
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
                  defaultValue={assetStore.filter.get("QueryString")}
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
                content="Create new asset"
                color="red"
                size="mini"
                as={NavLink}
                to="/assets/create"
              />
            </div>
          </Grid.Column>
        </Grid.Row>
      </Grid>
    </>
  );
});
