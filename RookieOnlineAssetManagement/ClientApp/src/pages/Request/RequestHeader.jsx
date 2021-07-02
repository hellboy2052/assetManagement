import { observer } from "mobx-react-lite";
import { Grid, Input, Button, Checkbox } from "semantic-ui-react";
import {
  Dropdown,
  DropdownToggle,
  DropdownMenu,
  DropdownItem,
} from "reactstrap";
import { useState } from "react";
import DatePicker from "react-datepicker";
import { useStore } from "../../api/store";
export default observer(function RequestHeader() {
  const { returnStore } = useStore();
  const { filter, setFilter, setFilterDate, setQueryString } = returnStore;
  const [filterState, setFilterState] = useState(
    filter.get("State").length === 2 ? [0] : filter.get("State")
  );
  const [dropdownOpen, setDropdownOpen] = useState(false);
  const toggle = () => setDropdownOpen((prevState) => !prevState);

  const handleStateFilter = (e, { value }) => {
    if (value === 0) {
      setFilterState([value]);
      setFilter([1, 2]);
    } else if (filterState.includes(value) && filterState.length === 1) {
      setFilterState([0]);
      setFilter([1, 2]);
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
    setQueryString(e.currentTarget.previousSibling.value);
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
                    <DropdownItem>
                      <Checkbox
                        label={"WaitingForReturning"}
                        value={1}
                        checked={filterState.includes(1)}
                        onClick={handleStateFilter}
                      />
                    </DropdownItem>
                    <DropdownItem>
                      <Checkbox
                        label={"Completed"}
                        value={2}
                        checked={filterState.includes(2)}
                        onClick={handleStateFilter}
                      />
                    </DropdownItem>
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
                {filter.get("ReturnedDate") && (
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
                  placeholderText="Returned Date"
                  showMonthDropdown={true}
                  showYearDropdown={true}
                  scrollableYearDropdown={false}
                  selected={
                    (filter.get("ReturnedDate") &&
                      new Date(filter.get("ReturnedDate"))) ||
                    null
                  }
                />
              </div>
            </div>
          </Grid.Column>
          <Grid.Column></Grid.Column>
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
        </Grid.Row>
      </Grid>
    </div>
  );
});
