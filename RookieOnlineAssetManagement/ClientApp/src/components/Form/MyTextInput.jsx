import { useField } from "formik";
import React from "react";
import { Form, Grid, Label } from "semantic-ui-react";

export default function MyTextInput(prop) {
  const [field, meta] = useField(prop.name);

  return (
    <div className="mb-4">
      <Grid>
        <Grid.Column verticalAlign="middle" width={5}>
          <label>{prop.label}:</label>
        </Grid.Column>
        <Grid.Column verticalAlign="middle" width={11}>
          <Form.Field error={meta.touched && !!meta.error}>
            <input
              {...field}
              type={prop.type || "text"}
              name={prop.name || ""}
              style={{ padding: "4px" }}
              disabled={prop.disabled || false}
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
