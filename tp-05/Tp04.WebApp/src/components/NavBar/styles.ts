import styled from 'styled-components'

export const Container = styled.header`
  width: 100%;
  height: 50px;

  margin: 10px 0;
  padding: 0 10px;

  display: flex;
  align-items: center;
  justify-content: space-between;
`

export const ReturnContainer = styled.div`
  width: fit-content;

  padding: 4px 10px;

  display: flex;
  align-items: center;
  justify-content: space-between;

  border-radius: 20px;

  transition: background-color 0.25s ease;

  cursor: pointer;

  &:hover {
    background-color: #dddddd;
  }

  svg {
    margin-right: 10px;
  }

  strong {
    font-weight: 700px;
  }
`
