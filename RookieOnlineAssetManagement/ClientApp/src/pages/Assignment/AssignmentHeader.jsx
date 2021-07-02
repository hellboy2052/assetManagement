import { observer } from "mobx-react-lite";
import { Grid, Input, Button, Checkbox } from "semantic-ui-react";
import {
  Dropdown,
  DropdownToggle,
  DropdownMenu,
  DropdownItem,
} from "reactstrap";
import { NavLink } from "react-router-dom";
import React, { useState } from "react";
import { AssignmentStateOptions } from "../../components/option/AssignmentStateOption";
import DatePicker from "react-datepicker";
import { useStore } from "../../api/store";

const state = ["Accepted", "WaitingForAcceptance", "Declined"];
export default observer(function AssginmentHeader() {
  const { assignmentStore } = useStore();
  const { filter, setFilter, setSearchQuery, setFilterDate } = assignmentStore;
  const [filterState, setFilterState] = useState(
    filter.get("State").length === 3 ? [0] : filter.get("State")
  );
  const [dropdownOpen, setDropdownOpen] = useState(false);
  const toggle = () => setDropdownOpen((prevState) => !prevState);

  const handleStateFilter = (e, { value }) => {
    if (value === 0) {
      setFilterState([value]);
      setFilter(
        state.map(
          Map.prototype.get,
          new Map(
            AssignmentStateOptions.map(({ text, value }) => [text, value])
          )
        )
      );
    } else if (filterState.includes(value) && filterState.length === 1) {
      setFilterState([0]);
      setFilter(
        state.map(
          Map.prototype.get,
          new Map(
            AssignmentStateOptions.map(({ text, value }) => [text, value])
          )
        )
      );
    } else {
      if (!filterState.includes(value)) {
        setFilterState([...filterState.filter((s) => s !== 0), value]);
        setFilter([...filterState.filter((s) => s !== 0), value]);
      } else {
        setFilterState([...filterState.filter((s) => s !== value)]);
        setFilter([...filterState.filter((s) => s !== value)]);
      }
    }
  };
  const handleSearch = (e) => {
    setSearchQuery(e.currentTarget.previousSibling.value);
  };
  const filterDate = (d) => {
    setFilterDate(d);
  };
  return (
    <div className="mb-4">
      <Grid columns={4}>
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
                    State
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
                        value={0}
                        checked={filterState.includes(0)}
                        onClick={handleStateFilter}
                      />
                    </DropdownItem>
                    {AssignmentStateOptions.map(
                      (s) =>
                        (s.text === "Accepted" ||
                          s.text === "WaitingForAcceptance" ||
                          s.text === "Declined") && (
                          <DropdownItem key={s.value}>
                            <Checkbox
                              label={s.text}
                              value={s.value}
                              checked={filterState.includes(s.value)}
                              onClick={handleStateFilter}
                            />
                          </DropdownItem>
                        )
                    )}
                  </DropdownMenu>
                </Dropdown>
              </div>
            </div>
          </Grid.Column>
          {/* Date filter */}
          <Grid.Column>
            <div className="input-group">
              <div
                style={{
                  display: "inherit",
                  border: "2px solid rgb(214 214 214)",
                  borderRadius: "5px",
                }}
              >
                {filter.get("AssignedDate") && (
                  <Button
                    icon="close"
                    size="mini"
                    color="red"
                    basic
                    onClick={() => setFilterDate("")}
                  />
                )}
                <DatePicker
                  className="btn btn-white text-start"
                  onSelect={filterDate}
                  placeholderText="Date"
                  showMonthDropdown={true}
                  showYearDropdown={true}
                  scrollableYearDropdown={false}
                  selected={
                    (filter.get("AssignedDate") &&
                      new Date(filter.get("AssignedDate"))) ||
                    null
                  }
                />
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
                  defaultValue={filter.get("QueryString")}
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
                content="Create new assignment"
                color="red"
                size="mini"
                as={NavLink}
                to="/assignments/create"
              />
            </div>
          </Grid.Column>
        </Grid.Row>
      </Grid>
    </div>
  );
});
