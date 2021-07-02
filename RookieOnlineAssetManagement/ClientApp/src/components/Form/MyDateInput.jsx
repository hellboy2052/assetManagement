import { useField } from "formik";
import React from "react";
import { Form, Grid, Label } from "semantic-ui-react";
import DatePicker from "react-datepicker";
import "./MyDateInput.css";

export default function MyDateInput(prop) {
  const [field, meta, helpers] = useField(prop.name);

  return (
    <div className="mb-4">
      <Grid>
        <Grid.Column verticalAlign="middle" width={5}>
          <label>{prop.label}:</label>
        </Grid.Column>
        <Grid.Column verticalAlign="middle" width={11}>
          <Form.Field error={meta.touched && !!meta.error}>
            <DatePicker
              {...field}
              {...prop}
              showYearDropdown
              selected={(field.value && new Date(field.value)) || null}
              onChange={(value) => helpers.setValue(value)}
              minDate={prop.minDate || null}
            />
          </Form.Field>
        </Grid.Column>
      </Grid>
      {meta.touched && meta.error ? (
        <Label
          basic
          color="red"
          style={{ marginTop: "10px", marginLeft: "122px" }}
        >
          {meta.error}
        </Label>
      ) : null}
    </div>
  );
}
